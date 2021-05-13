using System;
using ReportsComparer.ReportStructure;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ReportsComparer.ReportViewFiles;

namespace ReportsComparer
{
    public static class ReportsComparer
    {
        public static IReport GetComparativeReport(IReport baseReport, IReport newReport)
        {
            var textsCompared = GetTextsCompared(baseReport.View.Texts, newReport.View.Texts);
            
            var tablesCompared = GetTablesCompared(baseReport.View.Tables, newReport.View.Tables);

            var responseTimeCumsum = GetChartsCumsum(baseReport.View.Charts, newReport.View.Charts);

            var outliersTexts = GetTextFromOutliersData(baseReport, newReport);

            var fullTexts = textsCompared.Union(outliersTexts).ToArray();

            var newView = new ReportView {Texts = fullTexts, Tables = tablesCompared, Charts = responseTimeCumsum};

            return new Report(newView);
        }
        
        public static Text[] GetTextsCompared(Text[] baseReportText, Text[] newReportText)
        {
            return baseReportText
                .Join(
                    newReportText,
                    repBase => repBase.Name,
                    repNew => repNew.Name,
                    (first, second) => 
                        new 
                        {
                            first.Name, 
                            FirstValue = float.Parse(first.Value, CultureInfo.InvariantCulture.NumberFormat), 
                            SecondValue = float.Parse(second.Value, CultureInfo.InvariantCulture.NumberFormat)
                        })
                .Select(x => new Text
                {
                    Name = x.Name, 
                    Value = (((x.SecondValue - x.FirstValue) / x.FirstValue) * 100).ToString(CultureInfo.InvariantCulture.NumberFormat)
                })
                .ToArray();
        }

        public static Table[] GetTablesCompared(Table[] baseReportTables, Table[] newReportTables)
        {
            return baseReportTables
                .Join(
                    newReportTables,
                    repBase => repBase.Name,
                    repNew => repNew.Name,
                    (first, second) => 
                        new
                        {
                            first.Name, 
                            first.Rows, 
                            second.Columns, 
                            FirstMatrix = first.Matrix, 
                            SecondMatrix = second.Matrix
                        })
                .Select(x => new Table
                {
                    Name = x.Name, 
                    Rows = x.Rows, 
                    Columns = x.Columns, 
                    Matrix = GetMatricesCompared(x.FirstMatrix, x.SecondMatrix)
                })
                .ToArray();
        }

        public static string[][] GetMatricesCompared(string[][] first, string[][] second)
        {
            var columnsCount = first.Length;
            var rowsCount = first[0].Length;

            var result = new string[columnsCount][];
            
            for (var i = 0; i < columnsCount; i++)
            {
                result[i] = new string[rowsCount];
                result[i][0] = first[i][0];
                for (var j = 1; j < rowsCount; j++)
                {
                    var firstElement = first[i][j];
                    var secondElement = second[i][j];

                    var firstParsed = float.Parse(firstElement, CultureInfo.InvariantCulture.NumberFormat);
                    var secondParsed = float.Parse(secondElement, CultureInfo.InvariantCulture.NumberFormat);

                    var value = (secondParsed - firstParsed) / secondParsed * 100;

                    var valueParsed = value.ToString(CultureInfo.InvariantCulture.NumberFormat);

                    result[i][j] = valueParsed;
                }
            }

            return result;
        }

        public static Chart[] GetChartsCumsum(Chart[] baseReportChart, Chart[] newReportChart)
        {
            return baseReportChart
                .Join(
                    newReportChart,
                    repBase => repBase.Name,
                    repNew => repNew.Name,
                    (first, second) => 
                        new
                        {
                            first.Name, 
                            first.ValueType,
                            FirstGraphs = first.Graphs, 
                            SecondGraphs = second.Graphs
                        })
                .Select(x => new Chart
                {
                    Name = x.Name, 
                    ValueType = x.ValueType, 
                    Graphs = GetGraphsCumsum(x.FirstGraphs, x.SecondGraphs)
                })
                .ToArray();
        }

        public static Graph[] GetGraphsCumsum(Graph[] baseReportGraphs, Graph[] newReportGraphs)
        {
            return baseReportGraphs
                .Join(
                    newReportGraphs,
                    repBase => repBase.Name,
                    repNew => repNew.Name,
                    (first, second) => 
                        new {first.Name, first.Options, FirstPoints = first.Points, SecondPoints = second.Points})
                .Select(x => new Graph
                {
                    Name = x.Name, 
                    Options = x.Options,
                    Points = GetGraphCumsum(x.FirstPoints, x.SecondPoints)
                })
                .ToArray();
        }

        public static IEnumerable<DataPoint> GetGraphCumsum(IEnumerable<DataPoint> baseReportGraph, IEnumerable<DataPoint> newReportGraph)
        {
            var result = new List<DataPoint>();
            var zipped = baseReportGraph
                .Zip(newReportGraph)
                .Select(x => (x.First.Timestamp, Math.Abs(x.First.Data - x.Second.Data)))
                .Aggregate((x, y) =>
                {
                    var acc = x.Item2 + y.Item2;
                    result.Add(new DataPoint(x.Timestamp, acc));
                    return (y.Timestamp, acc);
                });

            return result;
        }

        private static Text[] GetTextFromOutliersData(IReport baseReport, IReport newReport)
        {
            throw new NotImplementedException();
        }
    }
}
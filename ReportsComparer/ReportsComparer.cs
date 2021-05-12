using System;
using ReportsComparer.ReportStructure;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ReportsComparer.ReportViewFiles;

namespace ReportsComparer
{
    public class ReportsComparer
    {
        public IReport GetComparativeReport(IReport baseReport, IReport newReport)
        {
            var textsCompared = GetTextsCompared(baseReport.View.Texts, newReport.View.Texts);
            
            var tablesCompared = GetTablesCompared(baseReport.View.Tables, newReport.View.Tables);

            var responseTimeCumsum = GetChartsCumsum(baseReport.View.Charts, newReport.View.Charts);

            var outliersTexts = GetTextFromOutliersData(baseReport, newReport);

            var fullTexts = textsCompared.Union(outliersTexts).ToArray();

            var newView = new ReportView {Texts = fullTexts, Tables = tablesCompared, Charts = responseTimeCumsum};

            return new Report(newView);
        }
        
        private Text[] GetTextsCompared(Text[] baseReportText, Text[] newReportText)
        {
            return baseReportText
                .Join(
                    newReportText,
                    repBase => repBase.Name,
                    repNew => repNew.Name,
                    (first, second) => 
                        new {first.Name, FirstValue = int.Parse(first.Value), SecondValue = int.Parse(second.Value)})
                .Select(x => new Text {Name = x.Name, Value = (((x.SecondValue - x.FirstValue) / x.FirstValue) * 100).ToString()})
                .ToArray();
        }

        private Table[] GetTablesCompared(Table[] baseReportTables, Table[] newReportTables)
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

        private string[][] GetMatricesCompared(string[][] first, string[][] second)
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

                    var firstParsed = float.Parse(firstElement);
                    var secondParsed = float.Parse(secondElement);

                    var value = (secondParsed - firstParsed) / secondParsed * 100;

                    var valueParsed = value.ToString();

                    result[i][j] = valueParsed;
                }
            }

            return result;
        }

        private Chart[] GetChartsCumsum(Chart[] baseReportChart, Chart[] newReportChart)
        {
            throw new NotImplementedException();
        }

        private Text[] GetTextFromOutliersData(IReport baseReport, IReport newReport)
        {
            throw new NotImplementedException();
        }
    }
}
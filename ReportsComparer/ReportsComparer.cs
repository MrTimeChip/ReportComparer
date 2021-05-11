using System;
using ReportsComparer.ReportStructure;
using System.Collections.Generic;
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

            return new Report(new ReportView());
        }
        
        private Text[] GetTextsCompared(Text[] baseReportText, Text[] newReportText)
        {
            throw new NotImplementedException();
        }

        private Table[] GetTablesCompared(Table[] baseReportTables, Table[] newReportTables)
        {
            throw new NotImplementedException();
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
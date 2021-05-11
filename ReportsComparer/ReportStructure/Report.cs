using System.Collections.Generic;
using ReportsComparer.ReportViewFiles;

namespace ReportsComparer.ReportStructure
{
    public class Report: IReport
    {
        public ReportView View { get; }

        public IEnumerable<DataPoint> AverageResponseTime =>
            View.GetFrom("Average response time", "Average response time", 0);

        public IEnumerable<DataPoint> GetContextOutliers(IReport other)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DataPoint> GetOutliers(string chartName, string graphName, int from)
        {
            throw new System.NotImplementedException();
        }

        public Report(ReportView view)
        {
            View = view;
        }
    }
}
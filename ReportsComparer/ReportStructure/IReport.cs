using System.Collections.Generic;
using ReportsComparer.ReportViewFiles;

namespace ReportsComparer.ReportStructure
{
    public interface IReport
    {
        ReportView View { get; }
        IEnumerable<DataPoint> AverageResponseTime { get; }
        IEnumerable<DataPoint> GetContextOutliers(IReport other);
        IEnumerable<DataPoint> GetOutliers(string chartName, string graphName, int from);
    }
}
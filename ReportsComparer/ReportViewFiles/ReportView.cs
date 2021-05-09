using System.Collections.Generic;
using System.Linq;

namespace ReportsComparer.ReportViewFiles
{
    public class ReportView
    {
        public Text[] Texts { get; set; }
        public Table[] Tables { get; set; }
        public Chart[] Charts { get; set; }

        public Metrics Metrics { get; set; }

        public IEnumerable<DataPoint> GetFrom(string chartName, string graphName, int from)
        {
            return Charts?.FirstOrDefault(chart => chart.Name == chartName)?
                .Graphs?.FirstOrDefault(graph => graph.Name == graphName)?
                .Points?.Skip(from) ?? Enumerable.Empty<DataPoint>();
        }
    }

    public class Metrics
    {
        public Chart[] AgentsSysMetrics { get; set; }

        public Chart[] TargetAppSysMetrics { get; set; }
    }
}
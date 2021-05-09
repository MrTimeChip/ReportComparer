using ReportsComparer.Data;

namespace ReportsComparer.ReportStructure
{
    public class Report: IReport
    {
        public TimeSeries Latency { get; set; }
    }
}
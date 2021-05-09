using ReportsComparer.Data;

namespace ReportsComparer.ReportStructure
{
    public interface IReport
    {
        TimeSeries Latency { get; set; }
    }
}
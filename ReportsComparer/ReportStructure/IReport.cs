using ReportsComparer.Data;

namespace ReportsComparer.ReportStructure
{
    public interface IReport
    {
        TimeSeries TimeSeries { get; set; }
        TimeSeries Outliers { get; }
    }
}
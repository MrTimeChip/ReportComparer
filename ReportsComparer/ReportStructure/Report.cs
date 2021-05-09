using ReportsComparer.Data;

namespace ReportsComparer.ReportStructure
{
    public class Report: IReport
    {
        public TimeSeries TimeSeries { get; set; }
        public TimeSeries Outliers { get; }
    }
}
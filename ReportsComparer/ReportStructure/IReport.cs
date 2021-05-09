using ReportsComparer.ReportViewFiles;

namespace ReportsComparer.ReportStructure
{
    public interface IReport
    {
        public ReportView View { get; set; }
    }
}
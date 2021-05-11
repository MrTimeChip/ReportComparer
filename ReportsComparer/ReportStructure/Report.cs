using ReportsComparer.ReportViewFiles;

namespace ReportsComparer.ReportStructure
{
    public class Report: IReport
    {
        public ReportView View { get; }

        public Report(ReportView view)
        {
            View = view;
        }
    }
}
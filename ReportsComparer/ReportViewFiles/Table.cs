namespace ReportsComparer.ReportViewFiles
{
    public class Table : BaseVariable
    {
        public string[] Rows { get; set; }
        public string[] Columns { get; set; }
        public string[][] Matrix { get; set; }
    }
}
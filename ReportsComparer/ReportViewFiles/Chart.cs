namespace ReportsComparer.ReportViewFiles
{
    public class Chart : BaseVariable
    {
        public string
            ValueType { get; set; } 

        public Graph[] Graphs { get; set; }
    }
}
using System.Collections.Generic;

namespace ReportsComparer.ReportViewFiles
{
    public class Graph : BaseVariable
    {
        public IEnumerable<DataPoint> Points { get; set; }

        public GraphVisualOptions Options { get; set; }
    }
}
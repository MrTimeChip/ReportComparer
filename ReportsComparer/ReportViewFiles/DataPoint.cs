using Newtonsoft.Json;

namespace ReportsComparer.ReportViewFiles
{
    public struct DataPoint
    {
        public readonly long Timestamp;
        public readonly double Data;

        [JsonConstructor]
        public DataPoint(long timestamp, double data)
        {
            Timestamp = timestamp;
            Data = data;
        }

        public override string ToString()
        {
            return $"{Timestamp} : {Data}";
        }
    }
}
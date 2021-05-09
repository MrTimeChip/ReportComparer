using System.Collections.Generic;

namespace ReportsComparer.Data
{
    public class TimeSeries
    {
        public List<DataPoint> Values { get; }
        
        public List<DataPoint> Outliers { get; }

        public TimeSeries(List<DataPoint> values)
        {
            Values = values;
        }
    }
}
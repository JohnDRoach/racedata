using System.Collections.Generic;

namespace GaugeCreator.Data
{
    public enum Types
    {
        Danny,
        DannyExtended,
        JD
    }

    public class DataSet
    {
        public DataHeader DataHeader { get; set; }
        public IEnumerable<DataLine> DataLines { get; set; }

        public static DataSet Load(string dataFileName, Types dataType)
        {
            switch (dataType)
            {
                case Types.Danny:
                    return Data.Type.Danny.LoadDataSetFromFile(dataFileName);
                case Types.DannyExtended:
                    return Data.Type.DannyExtended.LoadDataSetFromFile(dataFileName);
                case Types.JD:
                    return Data.Type.JD.LoadDataSetFromFile(dataFileName);
                default:
                    return new DataSet();
            }
        }
    }

    public class DataHeader
    {
        public string Driver { get; set; }
        public string Location { get; set; }
    }

    public class DataLine
    {
        public double LapTime { get; set; }
        public double Time { get; set; }
        public double Speed { get; set; }
        public int Rpm { get; set; }
        public int Lap { get; set; }
        public double GForcex { get; set; }
        public double GForcey { get; set; }
        public int Throttle { get; set; }
        public int Brake { get; set; }
    }
}

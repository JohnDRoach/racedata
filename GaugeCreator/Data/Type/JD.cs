using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GaugeCreator.Data.Type
{
    public class JD
    {
        public static DataSet LoadDataSetFromFile(string fileName)
        {
            DataSet set = new DataSet();

            DataHeader header = new DataHeader();
            header.Driver = "John Roach";
            header.Location = "Home Office";

            set.DataHeader = header;
            set.DataLines = GetDataLinesFrom(fileName);

            return set;
        }

        //Time (ms),LapTime (ms),Lap,Rear Speed (km/h),X Accel (g),Y Accel (g),Z Accel (g)
        //10904,0,0,0,-0.41,0.07,0.95
        private static IEnumerable<DataLine> GetDataLinesFrom(string fileName)
        {
            IList<DataLine> lines = new List<DataLine>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] splits = line.Split(',');

                        DataLine dataLine = new DataLine();
                        dataLine.Time = double.Parse(splits[0])/1000.0;
                        dataLine.LapTime = double.Parse(splits[1]);
                        dataLine.Lap = int.Parse(splits[2]);
                        dataLine.Speed = double.Parse(splits[3]);
                        dataLine.GForcex = double.Parse(splits[4]);
                        dataLine.GForcey = double.Parse(splits[5]);

                        lines.Add(dataLine);
                    }
                }
            }

            return lines;
        }
    }
}

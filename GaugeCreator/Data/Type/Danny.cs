using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GaugeCreator.Data.Type
{
    public class Danny
    {
        public static DataSet LoadDataSetFromFile(string fileName)
        {
            DataSet set = new DataSet();

            DataHeader header = new DataHeader();
            header.Driver = "Danny Bujna";
            header.Location = "King Edward Park";

            set.DataHeader = header;
            set.DataLines = GetDataLinesFrom(fileName);

            return set;
        }

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
                        dataLine.Time = double.Parse(splits[0]);
                        dataLine.Speed = double.Parse(splits[5]);
                        dataLine.GForcex = double.Parse(splits[6]);
                        dataLine.GForcey = double.Parse(splits[7]);

                        lines.Add(dataLine);
                    }
                }
            }

            return lines;
        }
    }
}

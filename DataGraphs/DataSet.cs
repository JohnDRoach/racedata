using ZedGraph;

namespace DataGraphs
{
    internal class DataSet
    {
        public string Title { get; private set; }
        public string XAxisTitle { get; private set; }
        public string YAxisTitle { get; private set; }
        public PointPairList Points { get; private set; }

        public DataSet(string title, string xAxisTitle, string yAxisTitle)
        {
            Title = title;
            XAxisTitle = xAxisTitle;
            YAxisTitle = yAxisTitle;

            Points = new PointPairList();
        }

        public void Add(double x, double y)
        {
            Points.Add(x,y);
        }
    }
}

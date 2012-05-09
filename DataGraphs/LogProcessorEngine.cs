using System;
using System.Collections.Generic;
using System.Drawing;
using DataGraphs;
using sharpPDF;
using sharpPDF.Enumerators;
using ZedGraph;

internal class LogProcessorEngine
{
    private readonly string fileName;
    private readonly IList<string> titlePageStrings;
    private readonly string headerRow;
    private readonly IList<string> data;
    private readonly int timeColumn;

    private pdfDocument logDocument;

    public LogProcessorEngine(string fileName, IList<string> titlePageStrings, string headerRow, IList<string> data, int timeColumn)
    {
        this.fileName = fileName;
        this.titlePageStrings = titlePageStrings;
        this.headerRow = headerRow;
        this.data = data;
        this.timeColumn = timeColumn;

        logDocument = new pdfDocument("Race Data Test", "John Roach");
    }

    public void Start()
    {
        AddTitlePage(titlePageStrings);

        IEnumerable<DataSet> dataSets = GetDataSetsFrom(headerRow, data, timeColumn);

        foreach (var dataSet in dataSets)
        {
            Image image = GetGraphFor(dataSet);
            image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pdfPage page = logDocument.addPage();
            page.addImage(image, 5, 5);
        }

        logDocument.createPDF(fileName);
    }

    private void AddTitlePage(IList<string> strings)
    {
        if (strings.Count > 0)
        {
            pdfPage titlePage = logDocument.addPage();

            ICollection<paragraphLine> paragraphLines = new List<paragraphLine>();

            foreach (var str in strings)
            {
                string formattedStr = str.Replace(",", ": ");
                paragraphLines.Add(new paragraphLine(formattedStr, 28, 0));
            }

            titlePage.addParagraph(paragraphLines, 20, 650, predefinedFont.csTimes, 14);
        }
    }

    private IEnumerable<DataSet> GetDataSetsFrom(string titles, IList<string> data, int timeColumn)
    {
        IDictionary<int, DataSet> dataSets = new Dictionary<int, DataSet>();

        string[] titleStrings = titles.Split(',');

        string timeTitle = titleStrings[timeColumn - 1];

        // Grab titles except for time column, put that in a time column variable
        for (int i = 0; i < titleStrings.Length; ++i)
        {
            if (i != (timeColumn - 1))
            {
                dataSets.Add(i, new DataSet(titleStrings[i], timeTitle, titleStrings[i]));
            }
        }

        // For each data
        foreach (string dataElement in data)
        {
            //   split into strings
            string[] splits = dataElement.Split(',');
            //   grab time data
            string timeData = splits[timeColumn - 1];
            //     For each data element in split string
            for (int i = 0; i < splits.Length; ++i)
            {
                //       If Not Time Column
                if(i != (timeColumn - 1))
                {
                    try
                    {
                        //         Add time and data element to corresponding data set
                        dataSets[i].Add(double.Parse(timeData), double.Parse(splits[i]));
                    }
                    catch (Exception)
                    {
                        dataSets[i].Add(double.Parse(timeData), 0);
                    }
                }
            }
        }

        return dataSets.Values;
    }

    private Image GetGraphFor(DataSet dataSet)
    {
        var pane = new GraphPane(new RectangleF(0, 0, 770, 600), dataSet.Title, dataSet.XAxisTitle, dataSet.YAxisTitle);

        pane.Title.IsVisible = false;
        pane.Legend.IsVisible = false;
        
        pane.XAxis.Type = AxisType.Linear;
        pane.XAxis.Title.FontSpec.Size = 8;
        pane.XAxis.Scale.FontSpec.Size = 8;
        pane.XAxis.Scale.Min = dataSet.Points[0].X;
        pane.XAxis.Scale.Max = dataSet.Points[dataSet.Points.Count - 1].X;
        
        pane.YAxis.Title.FontSpec.Size = 8;
        pane.YAxis.Scale.FontSpec.Size = 8;

        var curve = pane.AddCurve(dataSet.Title, dataSet.Points, Color.Red, SymbolType.None);

        curve.Line.StepType = StepType.NonStep;
        //curve.Line.IsSmooth = true;

        var bitmap = new Bitmap(1, 1);
        var graphics = Graphics.FromImage(bitmap);
        pane.AxisChange(graphics);

        return pane.GetImage();
    }
}

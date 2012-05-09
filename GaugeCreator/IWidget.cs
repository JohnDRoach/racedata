using System.Drawing;
using GaugeCreator.Data;

namespace GaugeCreator
{
    public interface IWidget
    {
        void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint);

        //string GetCaption();
        //double GetWidth();
        //double GetHeight();
    }
}

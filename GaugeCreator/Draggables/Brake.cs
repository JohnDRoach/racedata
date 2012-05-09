using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    class Brake : DraggableBarBase, IWidget
    {
        public Brake()
        {
            SetText("Brake");
            MinValue = -100;
            MaxValue = 100;
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawBar(pane, dataPoint.Brake, Color.Red, string.Format("{0:0.00}", dataPoint.Brake), Orientation.BottomToTop);
        }
    }
}

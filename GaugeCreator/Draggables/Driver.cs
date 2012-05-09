using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class Driver : DraggableTextBase, IWidget
    {
        public Driver()
        {
            SetText("Driver");
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, header.Driver);
        }
    }
}

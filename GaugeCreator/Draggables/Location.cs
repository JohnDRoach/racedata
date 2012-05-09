using System.Drawing;
using GaugeCreator.Draggables.Base;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    public class Location : DraggableTextBase, IWidget
    {
        public Location()
        {
            SetText("Location");
        }

        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, header.Location);
        }
    }
}

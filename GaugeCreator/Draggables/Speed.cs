using System.Drawing;
using GaugeCreator.Data;

namespace GaugeCreator.Draggables
{
    class Speed : Base.DraggableTextBase, IWidget
    {
        public Speed()
        {
            SetText("Speed km/h");
        }


        public void DrawYourselfOnThis(Graphics pane, DataHeader header, DataLine dataPoint)
        {
            DrawText(pane, string.Format("{0:0.00} km/h",  dataPoint.Speed));
        }
    }
}

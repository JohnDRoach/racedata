using System.Drawing;

namespace TestingVideo.Widgets
{
    class GForce : IWidgethaha
    {
        public void DrawYourselfOnThis(Graphics pane)
        {
            Pen thickPen = new Pen(Color.Blue, 3);
            Pen thinPen = new Pen(Color.DarkViolet, 1);

            Rectangle circleRect = new Rectangle(300, 0, 100, 100);
            pane.DrawArc(thickPen, circleRect, 0, 360);

            pane.DrawLine(thinPen, new Point(350, 0), new Point(350, 100));
            pane.DrawLine(thinPen, new Point(300, 50), new Point(400, 50));

            pane.DrawPie(thinPen, new Rectangle(300 - 2, 50 - 2 , 2, 2), 0, 360);
        }
    }
}

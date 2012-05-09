using System.Drawing;

namespace TestingVideo.Widgets
{
    class Brake : IWidgethaha
    {
        private int value;

        public Brake(int value)
        {
            this.value = value % 100;
        }

        public void DrawYourselfOnThis(Graphics pane)
        {
            Pen pen = new Pen(Color.Red, 10);

            const int totalHeight = 50;
            const int totalWidth = 10;
            const int startx = 200;
            const int starty = 20;

            int width = totalWidth;
            int height = (int)(totalHeight * (value / 100.0));
            int x = startx;
            int y = starty + (totalHeight - height);

            pane.DrawRectangle(pen, x, y, width, height);

            pane.DrawString("B", GaugeDisplay.DefaultFont, Brushes.Black, startx - 5, starty + totalHeight + 2);
        }
    }
}

using System.Drawing;

namespace TestingVideo.Widgets
{
    class Rpm : IWidgethaha
    {
        private readonly int value;

        public Rpm(int value)
        {
            this.value = value;
        }

        public void DrawYourselfOnThis(Graphics pane)
        {
            pane.DrawString("RPM: " + value, GaugeDisplay.DefaultFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
        }
    }
}

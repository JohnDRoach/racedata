using System.Drawing;

namespace TestingVideo.Widgets
{
    class IntakeTemp : IWidgethaha
    {
        private readonly int value;

        public IntakeTemp(int value)
        {
            this.value = value;
        }

        public void DrawYourselfOnThis(Graphics pane)
        {
            pane.DrawString("Intake Temp: " + value, GaugeDisplay.DefaultFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 50);
        }
    }
}

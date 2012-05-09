using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TestingVideo
{
    public class GaugeDisplay
    {
        public static readonly Font DefaultFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

        private const int Width = 400;
        private const int Height = 100;

        private int counter = 0;
        private Random rand = new Random();

        public GaugeDisplay(int framerate)
        {
        }

        public Bitmap GetNextFrame()
        {
            var frameImage = new Bitmap(Width, Height);

            var frame = Graphics.FromImage(frameImage);

            frame.Clear(Color.White);

            frame.SmoothingMode = SmoothingMode.AntiAlias;

            frame.TextRenderingHint = TextRenderingHint.AntiAlias;

            foreach (var widget in Widgets)
            {
                widget.DrawYourselfOnThis(frame);
            }

            frame.Flush();

            counter++;

            return frameImage;
        }

        public IEnumerable<IWidgethaha> Widgets { get; set; }

        //private IEnumerable<IWidget> GetWidgets()
        //{
        //    ICollection<IWidget> widgets = new List<IWidget>();
            
        //    widgets.Add(new Widgets.GForce());
        //    widgets.Add(new Widgets.Rpm(counter));
        //    widgets.Add(new Widgets.IntakeTemp(rand.Next()));
        //    widgets.Add(new Widgets.Throttle(rand.Next()));
        //    widgets.Add(new Widgets.Brake(rand.Next()));

        //    return widgets;
        //}
    }
}

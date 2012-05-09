using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using GaugeCreator.Data;

namespace GaugeCreator
{
    public class FrameGrabber
    {
        private readonly IEnumerable<IWidget> widgets;

        public FrameGrabber(IEnumerable<IWidget> widgets)
        {
            this.widgets = widgets;
        }

        public Bitmap GetFrameFor(DataHeader dataHeader, DataLine dataLine)
        {
            var frameImage = new Bitmap(420, 240);

            var frame = Graphics.FromImage(frameImage);

            frame.Clear(Color.White);

            frame.SmoothingMode = SmoothingMode.AntiAlias;

            frame.TextRenderingHint = TextRenderingHint.AntiAlias;

            foreach (var widget in widgets)
            {
                widget.DrawYourselfOnThis(frame, dataHeader, dataLine);
            }

            frame.Flush();

            return frameImage;
        }

        // New function
        public Bitmap GetFrameFor(DataHeader dataHeader, DataLine dataLine, Bitmap existingFrame)
        {
            var frame = Graphics.FromImage(existingFrame);

            frame.SmoothingMode = SmoothingMode.AntiAlias;

            frame.TextRenderingHint = TextRenderingHint.AntiAlias;

            foreach (var widget in widgets)
            {
                widget.DrawYourselfOnThis(frame, dataHeader, dataLine);
            }

            frame.Flush();

            return existingFrame;
        }
    }
}

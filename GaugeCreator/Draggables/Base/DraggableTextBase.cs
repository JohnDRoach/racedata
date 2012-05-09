using System.Drawing;

namespace GaugeCreator.Draggables.Base
{
    public abstract class DraggableTextBase : DraggableBase
    {
        protected void DrawText(Graphics pane, string text)
        {
            Font font = GetFontForHeight();
            //font = GaugeDisplay.DefaultFont;

            //pane.DrawString(text, font, new SolidBrush(Color.Black), GetTopLeftX(), GetTopLeftY());
            pane.DrawString(text, font, Brushes.White, GetTopLeftX(), GetTopLeftY());
        }

        private Font GetFontForHeight()
        {
            return new Font(System.Drawing.FontFamily.GenericMonospace, GetHeight(), GraphicsUnit.Pixel);
        }
    }
}

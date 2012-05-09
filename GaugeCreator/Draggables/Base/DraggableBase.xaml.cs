using System.Windows.Controls;

namespace GaugeCreator.Draggables.Base
{
    /// <summary>
    /// Interaction logic for DraggableBase.xaml
    /// </summary>
    public partial class DraggableBase : UserControl
    {
        public DraggableBase()
        {
            InitializeComponent();

            Canvas.SetLeft(this, 0);
            Canvas.SetTop(this, 0);
            //Canvas.SetBottom(this, 0);
            //Canvas.SetRight(this, 0);
        }

        public void SetText(string text)
        {
            DisplayText.Text = text;
        }

        public void FreezeAttributes()
        {
            height = (int) ActualHeight;
            width = (int) ActualWidth;
            topLeftX = (float) Canvas.GetLeft(this);
            topLeftY = (float) Canvas.GetTop(this);
        }

        private int height = 0;
        public int GetHeight()
        {
            //return (int)ActualHeight;
            return height;
        }

        private int width = 0;
        public int GetWidth()
        {
            //return (int)ActualWidth;
            return width;
        }

        private float topLeftX = 0;
        public float GetTopLeftX()
        {
            //return (float)Canvas.GetLeft(this);
            return topLeftX;
        }

        private float topLeftY = 0;
        public float GetTopLeftY()
        {
            //return (float)Canvas.GetTop(this);
            return topLeftY;
        }
    }
}

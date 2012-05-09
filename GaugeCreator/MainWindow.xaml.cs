using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using adorners;
using AviFile;
using Microsoft.Win32;
using GaugeCreator.Data;
using Point = System.Windows.Point;
using System.IO;

namespace GaugeCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AdornerLayer aLayer;

        bool _isDown;
        bool _isDragging;
        bool selected = false;
        UIElement selectedElement = null;

        Point _startPoint;
        private double _originalLeft;
        private double _originalTop;

        private string inputFileName = "TestVideo1.avi";
        private string dataFileName = "TestVideo1.data";
        private string outputFileName = "Test.avi";

        public MainWindow()
        {
            InitializeComponent();
            InitialiseDataTypeCombo();
        }

        private void InitialiseDataTypeCombo()
        {
            DataTypeCombo.Items.Add(Data.Types.JD);
            DataTypeCombo.Items.Add(Data.Types.Danny);
            DataTypeCombo.Items.Add(Data.Types.DannyExtended);
            DataTypeCombo.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MouseLeftButtonDown += new MouseButtonEventHandler(Window1_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);
            this.MouseMove += new MouseEventHandler(Window1_MouseMove);
            this.MouseLeave += new MouseEventHandler(Window1_MouseLeave);

            VideoViewPort.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(myCanvas_PreviewMouseLeftButtonDown);
            VideoViewPort.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(DragFinishedMouseHandler);
        }

        // Handler for drag stopping on leaving the window
        void Window1_MouseLeave(object sender, MouseEventArgs e)
        {
            StopDragging();
            e.Handled = true;
        }

        // Handler for drag stopping on user choise
        void DragFinishedMouseHandler(object sender, MouseButtonEventArgs e)
        {
            StopDragging();
            e.Handled = true;
        }

        // Method for stopping dragging
        private void StopDragging()
        {
            if (_isDown)
            {
                _isDown = false;
                _isDragging = false;
            }
        }

        // Hanler for providing drag operation with selected element
        void Window1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDown)
            {
                if ((_isDragging == false) &&
                    ((Math.Abs(e.GetPosition(VideoViewPort).X - _startPoint.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(e.GetPosition(VideoViewPort).Y - _startPoint.Y) > SystemParameters.MinimumVerticalDragDistance)))
                    _isDragging = true;

                if (_isDragging)
                {
                    Point position = Mouse.GetPosition(VideoViewPort);
                    Canvas.SetTop(selectedElement, position.Y - (_startPoint.Y - _originalTop));
                    Canvas.SetLeft(selectedElement, position.X - (_startPoint.X - _originalLeft));
                }
            }
        }

        // Handler for clearing element selection, adorner removal
        void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selected)
            {
                selected = false;

                if (selectedElement != null)
                {
                    aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }
        }

        // Handler for element selection on the canvas providing resizing adorner
        void myCanvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Remove selection on clicking anywhere the window
            if (selected)
            {
                selected = false;
                if (selectedElement != null)
                {
                    // Remove the adorner from the selected element
                    aLayer.Remove(aLayer.GetAdorners(selectedElement)[0]);
                    selectedElement = null;
                }
            }

            // If any element except canvas is clicked, 
            // assign the selected element and add the adorner
            if (e.Source != VideoViewPort)
            {
                _isDown = true;
                _startPoint = e.GetPosition(VideoViewPort);

                selectedElement = e.Source as UIElement;

                _originalLeft = Canvas.GetLeft(selectedElement);
                _originalTop = Canvas.GetTop(selectedElement);

                aLayer = AdornerLayer.GetAdornerLayer(selectedElement);
                aLayer.Add(new ResizingAdorner(selectedElement));

                selected = true;
                e.Handled = true;
            }
        }

        private void GenerateClick(object sender, RoutedEventArgs e)
        {
            UIElement[] array = new UIElement[VideoViewPort.Children.Count];
            VideoViewPort.Children.CopyTo(array, 0);
            IEnumerable<IWidget> widgets = array.Select(x => (IWidget)x);

            foreach (var widget in widgets)
            {
                Draggables.Base.DraggableBase variable = widget as Draggables.Base.DraggableBase;
                if (variable != null)
                {
                    variable.FreezeAttributes();
                }
            }

            //VideoCreator creator = new VideoCreator(GetData(), widgets, 25); // Danny's
            VideoCreator creator = new VideoCreator(GetData(), widgets, 30); // JD's
            creator.ProgressUpdate += OnProgressUpdate;
            creator.FinishedProcessing += OnFinishedProcessing;

            GenerateButton.IsEnabled = false;

            FileInfo inputFile = new FileInfo(inputFileName);

            Task.Factory.StartNew(() => creator.Create(inputFileName, Path.Combine(inputFile.Directory.FullName, outputFileName)));
        }

        private DataSet GetData()
        {
            return Data.DataSet.Load(dataFileName, (Data.Types)DataTypeCombo.SelectedItem);
        }

        private void LoadVideoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            inputFileName = dialog.FileName;

            //Bitmap image = VideoHelper.GetFirstFrameFromVideoFile(inputFileName);
            //BackgroundImage.ImageSource = VideoHelper.ConvertBitmapToBitmapSource(image);
        }

        private void LoadDataClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            dataFileName = dialog.FileName;
        }

        private void OnProgressUpdate(object sender, ProgressUpdate e)
        {
            Dispatcher.BeginInvoke(new Action(() => UpdateProgress(e.Progress)));
        }

        private void UpdateProgress(int value)
        {
            Progress.Text = string.Format("{0}%", value);
        }

        private void OnFinishedProcessing(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(EnableGenerateButton));
        }

        private void EnableGenerateButton()
        {
            GenerateButton.IsEnabled = true;
        }
    }
}

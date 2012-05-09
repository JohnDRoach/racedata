using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace DataGraphs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileName = string.Empty;
        private LogProcessor processor = null;

        public MainWindow()
        {
            InitializeComponent();
            MoreInit();
        }

        private void MoreInit()
        {
            StartButton.IsEnabled = false;
            TimeColumnTextBox.Text = "2";
            HeaderLineTextBox.Text = "11";
        }

        public string FileName
        {
            get 
            {
                return fileName;
            }

            set
            { 
                fileName = value;
                FileNameBlock.Text = fileName;
            }
        }

        private void OpenFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "txt files (*.csv)|*.csv|All files (*.*)|*.*";
            //dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select a log file";

            if(dialog.ShowDialog().Value)
            {
                FileName = dialog.FileName;
                StartButton.IsEnabled = true;
            }
            else
            {
                StartButton.IsEnabled = false;
                FileName = "Error!";
            }
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;

            processor = new LogProcessor(FileName, Int32.Parse(HeaderLineTextBox.Text), Int32.Parse(TimeColumnTextBox.Text));

            processor.FinishedProcessing += FinishedProcessing;
            
            processor.Start();

            Progress.Minimum = 0;
            Progress.Maximum = 100;
            Progress.IsIndeterminate = true;
        }

        private void FinishedProcessing(object sender, EventArgs e)
        {
            Progress.Dispatcher.BeginInvoke(new ThreadStart(() => Progress.IsIndeterminate = false));
            StartButton.Dispatcher.BeginInvoke(new ThreadStart(() => StartButton.IsEnabled = true));
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(processor != null)
            {
                processor.Stop();
            }
        }
    }
}

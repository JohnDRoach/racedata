using System.Windows.Controls;
using AviFile;

namespace TestingVideo
{
    public class Program
    {
        private const int Framerate = 25;  // framerate in seconds

        public static void Main(string[] args)
        {
            AviManager newFile = new AviManager("TestAvi.avi", false);

            GaugeDisplay display = new GaugeDisplay(Framerate);

            VideoStream newStream = newFile.AddVideoStream(false, Framerate, display.GetNextFrame());

            for (int n = 1; n < (Framerate * 5); n++)
            {
                newStream.AddFrame(display.GetNextFrame());
            }

            newFile.Close();
        }
    }
}

using AviFile;
using System.Drawing;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GaugeCreator
{
    public class VideoHelper
    {
        public static Bitmap GetFirstFrameFromVideoFile(string inputFileName)
        {
            var stream = GetStreamFromFile(inputFileName);
            var bitmap = GetFirstFrameFromStream(stream);
            stream.Close();
            return bitmap;
        }

        public static BitmapSource ConvertBitmapToBitmapSource(Bitmap image)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(image.Width, image.Height));
        }

        private static VideoStream GetStreamFromFile(string inputFileName)
        {
            AviManager sourceFile = new AviManager(inputFileName, true);
            VideoStream videoStream = sourceFile.GetVideoStream();
            return videoStream;
        }

        private static Bitmap GetFirstFrameFromStream(VideoStream stream)
        {
            stream.GetFrameOpen();
            Bitmap image = stream.GetBitmap(stream.FirstFrame);
            stream.GetFrameClose();
            return image;
        }
    }
}

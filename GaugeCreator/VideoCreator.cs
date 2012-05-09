using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using AviFile;
using GaugeCreator.Data;
using System.Diagnostics;
using System.Threading;

namespace GaugeCreator
{
    public class VideoCreator
    {
        private readonly DataSet data;
        private readonly IEnumerable<IWidget> widgets;
        private readonly FrameGrabber frameGrabber;

        private int framerate;
        
        private IEnumerator<DataLine> enumerator;
        private double timeCounter;  // in milliseconds
        private DataLine current;
        private DataLine next;

        public event EventHandler<ProgressUpdate> ProgressUpdate;
        public event EventHandler<EventArgs> FinishedProcessing;


        public VideoCreator(DataSet data, IEnumerable<IWidget> widgets, int framerate)
        {
            this.data = data;
            this.widgets = widgets;
            this.framerate = framerate;
            frameGrabber = new FrameGrabber(widgets);

            timeCounter = 0;
        }

        public void Create(string sourceFileName, string outputFileName)
        {
            DirectoryInfo info = new DirectoryInfo("c:\\temppics");
            info.Create();
            // NEW WAY
            // Grab sound from video file and store in temp location
            //Process ffmpeg = Process.Start("ffmpeg.exe", "-i \"" + sourceFileName + "\" -acodec pcm_s16le -ac 2 \"" + "c:\\tempaudio.wav" + "\"");
            // Call out to ffmpeg to split into picture files in a known temporary location
            
            //ffmpeg -i "input.mov" -an -f image2 "output_%05d.jpg"
            Process ffmpeg2 = Process.Start("ffmpeg.exe", "-i \"" + sourceFileName + "\" -an -r 25 -f image2 \"" + "c:\\temppics\\output_%06d.jpg" + "\"");
            ffmpeg2.WaitForExit(180000);
            // Create the new video with the first image
            // Iterate over remaining images
            // Add sound into new video file
            // Finish =)

            // OLD WAY
            //AviManager sourceFile = new AviManager(sourceFileName, true);
            //VideoStream videoStream = sourceFile.GetVideoStream();
            //framerate = (int)videoStream.FrameRate;
            //framerate = 24;

            //videoStream.GetFrameOpen();

            AviManager newFile = new AviManager(outputFileName, false);

            //VideoStream newStream = newFile.AddVideoStream(true, 25, GetNextFrame(videoStream.GetBitmap(videoStream.FirstFrame)));
            //VideoStream newStream = newFile.AddVideoStream(true, framerate, GetNextFrame(videoStream.GetBitmap(videoStream.FirstFrame)));
            string newSource = "c:\\temppics\\output_000001.jpg";
            VideoStream newStream = newFile.AddVideoStream(false, framerate, GetNextFrame(new Bitmap(newSource)));

            //FileInfo firstFile = new FileInfo(sourceFileName);
            //DirectoryInfo directory = firstFile.Directory;

            FileInfo[] files = info.GetFiles();

            for(int i = 1; i < files.Length; ++i)
            {
                try
                {
                    Bitmap baseImage = new Bitmap(files[i].FullName);
                    Bitmap nextFrame = GetNextFrame(baseImage);
                    newStream.AddFrame(nextFrame);

                    baseImage.Dispose();
                    nextFrame.Dispose();

                    if (ProgressUpdate != null)
                    {
                        ProgressUpdate(this, new ProgressUpdate((int)(i * 100.0 / files.Length)));
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

            //int errorCount = 0;

            //int frames = 25*60*3;

            //for (int n = 1; n < videoStream.CountFrames; ++n)
            //for (int n = 1; n < frames; ++n)
            //{
            //    try
            //    {
            //        //Bitmap baseImage = videoStream.GetBitmap(n);
            //        Bitmap baseImage = new Bitmap(720, 480);
            //        Bitmap nextFrame = GetNextFrame(baseImage);
            //        newStream.AddFrame(nextFrame);
            //        baseImage.Dispose();
            //        nextFrame.Dispose();
            //    }
            //    catch (Exception)
            //    {
            //        errorCount++;
            //    }
            //    finally
            //    {
            //        if (ProgressUpdate != null)
            //        {
            //            //ProgressUpdate(this, new ProgressUpdate((int)(n * 100.0 / videoStream.CountFrames) + (1000 * errorCount)));
            //            ProgressUpdate(this, new ProgressUpdate((int)(n * 100.0 / frames) + (1000 * errorCount)));
            //        }
            //    }
            //}

            //videoStream.GetFrameClose();
            //sourceFile.Close();

            newFile.Close();
            Thread.Sleep(1000);
            info.Delete(true);

            if (FinishedProcessing != null)
            {
                FinishedProcessing(this, EventArgs.Empty);
            }
        }

        private Bitmap GetNextFrame(Bitmap baseImage)
        {
            return frameGrabber.GetFrameFor(data.DataHeader, NextDataPoint(), baseImage);
        }

        private DataLine NextDataPoint()
        {
            if (enumerator == null)
            {
                enumerator = data.DataLines.GetEnumerator();
                
                current = GetNext();

                timeCounter = current.Time;
                
                next = GetNext();
            }
            else
            {
                while (next.Time <= timeCounter)
                {
                    current = next;
                    next = GetNext();
                }
            }

            timeCounter += 1.0/framerate;

            return current;
        }

        private DataLine GetNext()
        {
            if (!enumerator.MoveNext())
            {
                throw new Exception();
            }
            
            return enumerator.Current;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Linq;
using sharpPDF;
using sharpPDF.Enumerators;

namespace DataGraphs
{
    internal class LogProcessor
    {
        public event EventHandler<EventArgs> FinishedProcessing;

        private readonly string fileName;
        private readonly int headerRow;
        private readonly int timeColumn;
        private readonly Thread processingThread;
        
        public LogProcessor(string fileName, int headerRow, int timeColumn)
        {
            this.fileName = fileName;
            this.headerRow = headerRow;
            this.timeColumn = timeColumn;
            processingThread = new Thread(Process);
        }

        public void Start()
        {
            processingThread.Start();
        }

        public void Process()
        {
            try
            {
                ProcessFile();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
                // Kill File Handle here
            }
        }

        private void ProcessFile()
        {
            IList<string> lines = File.ReadAllLines(fileName);

            IList<string> firstPage = lines.Take(headerRow - 1).ToList();
            IList<string> data = lines.Skip(headerRow).ToList();

            LogProcessorEngine engine = new LogProcessorEngine(fileName + ".pdf", firstPage, lines[headerRow - 1], data, timeColumn);

            engine.Start();

            FinishedProcessing(this, EventArgs.Empty);
        }

        public void Stop()
        {
            processingThread.Abort();
        }
    }
}
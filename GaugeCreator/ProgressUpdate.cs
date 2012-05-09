using System;

namespace GaugeCreator
{
    public class ProgressUpdate : EventArgs
    {
        public ProgressUpdate(int value)
        {
            Progress = value;
        }

        public int Progress { get; private set; }
    }
}

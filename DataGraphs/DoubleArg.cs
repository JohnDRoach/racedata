using System;

namespace DataGraphs
{
    internal class DoubleArg : EventArgs
    {
        public double Value { get; private set; }

        public static DoubleArg Create(double value)
        {
            return new DoubleArg { Value = value };
        }
    }
}

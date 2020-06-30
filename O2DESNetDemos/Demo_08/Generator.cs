using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_08
{
    public class Generator : Sandbox
    {
        #region Statics
        public double HourlyRate { get; private set; }
        #endregion

        #region Dynamics
        public int Count { get; private set; } = 0;
        #endregion

        public Generator(double hourlyRate, int seed = 0) : base(seed)
        {
            HourlyRate = hourlyRate;
            Schedule(Generate);
        }

        void Generate()
        {
            if (Count > 0)
            {
                Console.WriteLine($"{ClockTime}\t{GetType().Name}\tGenerate. Count: {Count}");
                OnGenerate.Invoke();
            }
            Count++;
            Schedule(Generate, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyRate)));
        }

        public event Action OnGenerate = () => { };
    }
}

using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo1
{
    public class HelloWorld : Sandbox
    {
        #region Statics
        public double HourlyArrivalRate { get; private set; }
        #endregion

        #region Dynamics
        public int Count { get; private set; } = 0;
        #endregion

        public HelloWorld(double hourlyArrivalRate, int seed = 0) : base(seed)
        {
            HourlyArrivalRate = hourlyArrivalRate;
            Schedule(Arrive);
        }

        void Arrive()
        {
            Console.WriteLine($"{ClockTime}\tHello World #{Count}!");
            Count++;
            Schedule(Arrive, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyArrivalRate)));
        }
    }
}

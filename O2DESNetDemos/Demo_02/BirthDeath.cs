using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_02
{
    public class BirthDeath : Sandbox
    {
        #region Statics
        public double HourlyBirthRate { get; private set; }
        public double HourlyDeathRate { get; private set; }
        #endregion

        #region Dynamics
        public int Population { get; private set; } = 0;
        #endregion

        public BirthDeath(double hourlyBirthRate, double hourlyDeathRate, int seed = 0) : base(seed)
        {
            HourlyBirthRate = hourlyBirthRate;
            HourlyDeathRate = hourlyDeathRate;
            Schedule(Birth);
        }

        void Birth()
        {
            Population++;
            Console.WriteLine($"{ClockTime}\tBirth (Population: {Population})");
            /// Assume that each individual can only give birth for one-time
            Schedule(Birth, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyBirthRate)));
            Schedule(Death, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyDeathRate)));
        }

        void Death()
        {
            Population--;
            Console.WriteLine($"{ClockTime}\tDeath (Population: {Population})");
        }
    }
}

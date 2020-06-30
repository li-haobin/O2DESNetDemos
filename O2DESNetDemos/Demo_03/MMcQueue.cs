using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_03
{
    public class PingPongGame : Sandbox
    {
        #region Statics
        public double HourlyArrivalRate { get; private set; }
        public double HourlyServiceRate { get; private set; }
        public int Capacity { get; private set; }
        #endregion

        #region Dynamics
        public int InQueue { get; private set; } = 0;
        public int InService { get; private set; } = 0;
        #endregion

        public PingPongGame(double hourlyArrivalRate, double hourlyServiceRate, int capacity, int seed = 0) : base(seed)
        {
            HourlyArrivalRate = hourlyArrivalRate;
            HourlyServiceRate = hourlyServiceRate;
            Capacity = capacity;
            Schedule(Arrive);
        }

        void Arrive()
        {
            
            if (InService < Capacity)
            {
                InService++;
                Console.WriteLine($"{ClockTime}\tArrive and Start Service (In-Queue: {InQueue}, In-Service: {InService})");
                Schedule(Depart, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyServiceRate)));
            }
            else
            {
                InQueue++;
                Console.WriteLine($"{ClockTime}\tArrive and Start Service (In-Queue: {InQueue}, In-Service: {InService})");
            }
            Schedule(Arrive, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyArrivalRate)));            
        }

        void Depart()
        {
            if (InQueue > 0)
            {
                InQueue--;
                Console.WriteLine($"{ClockTime}\tDepart and Start Service for the next (In-Queue: {InQueue}, In-Service: {InService})");
                Schedule(Depart, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyServiceRate)));
            }
            else
            {
                InService--;
                Console.WriteLine($"{ClockTime}\tDepart (In-Queue: {InQueue}, In-Service: {InService})");
            }
        }
    }
}

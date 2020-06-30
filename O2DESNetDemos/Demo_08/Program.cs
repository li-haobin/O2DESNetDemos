using System;

namespace O2DESNetDemos.Demo_08
{
    class Program
    {
        public static void Run()
        {
            var sim = new TademQueue_Push(new TademQueue_Push.Statics 
            { 
                Capacity = 1,
                HourlyArrivalRate = 4,
                HourlyServiceRate = 5,
            }, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

using System;

namespace O2DESNetDemos.Demo6
{
    class Program
    {
        public static void Run()
        {
            var sim = new MMcQueue_Push(new MMcQueue_Push.Statics 
            { 
                Capacity = 1,
                HourlyArrivalRate = 4,
                HourlyServiceRate = 5,
            }, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

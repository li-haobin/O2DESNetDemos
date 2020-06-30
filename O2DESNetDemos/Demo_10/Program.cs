using System;

namespace O2DESNetDemos.Demo_10
{
    class Program
    {
        public static void Run()
        {
            var sim = new ConstrainedTademQueue_Push(new ConstrainedTademQueue_Push.Statics 
            { 
                QueueCapacity = 2,
                ServerCapacity = 1,
                HourlyArrivalRate = 4,
                HourlyServiceRate = 5,
            }, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

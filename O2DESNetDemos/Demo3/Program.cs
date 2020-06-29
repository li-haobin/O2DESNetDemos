using System;

namespace O2DESNetDemos.Demo3
{
    class Program
    {
        public static void Run()
        {
            var sim = new PingPongGame(hourlyArrivalRate: 5, hourlyServiceRate: 8, capacity: 2, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

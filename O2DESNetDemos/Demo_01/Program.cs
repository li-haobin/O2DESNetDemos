using System;

namespace O2DESNetDemos.Demo_01
{
    class Program
    {
        public static void Run()
        {
            var sim = new HelloWorld(hourlyArrivalRate: 10, seed: 3);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

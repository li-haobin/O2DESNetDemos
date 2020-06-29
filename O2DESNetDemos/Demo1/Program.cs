using System;

namespace O2DESNetDemos.Demo1
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

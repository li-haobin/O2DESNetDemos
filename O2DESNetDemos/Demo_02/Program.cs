using System;

namespace O2DESNetDemos.Demo_02
{
    class Program
    {
        public static void Run()
        {
            var sim = new BirthDeath(hourlyBirthRate: 12, hourlyDeathRate: 4, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

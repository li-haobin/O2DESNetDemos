using System;

namespace O2DESNetDemos.Demo4
{
    class Program
    {
        public static void Run()
        {
            var sim = new PingPongGame(new PingPongGame.Statics 
            { 
                Player1 = new PingPongPlayer.Statics
                {
                    Index = 0,
                    DelayTime_Expected = 2,
                    DelayTime_CV = 0.1,
                },
                Player2 = new PingPongPlayer.Statics
                {
                    Index = 1,
                    DelayTime_Expected = 1.8,
                    DelayTime_CV = 0.15,
                },
            }, seed: 0);
            while (sim.Run(10)) Console.ReadKey();
        }
    }
}

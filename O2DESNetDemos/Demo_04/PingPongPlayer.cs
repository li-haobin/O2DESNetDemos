using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_04
{
    public class PingPongPlayer : Sandbox
    {
        #region Statics
        public class Statics
        {
            public int Index { get; set; }
            /// <summary>
            /// In seconds
            /// </summary>
            public double DelayTime_Expected { get; set; }
            /// <summary>
            /// Coefficient of variation
            /// In seconds
            /// </summary>
            public double DelayTime_CV { get; set; }
        }
        public Statics Config { get; private set; }
        #endregion

        #region Dynamics
        public int Count { get; private set; } = 0;
        #endregion

        public PingPongPlayer(Statics config, int seed = 0) : base(seed)
        {
            Config = config;
        }

        void Send()
        {
            Console.WriteLine($"{ClockTime}\tSend. Player #{Config.Index}, Count: {Count}");
            OnSend.Invoke();
        }

        public void Receive()
        {
            Count++;
            Console.WriteLine($"{ClockTime}\tReceive. Player #{Config.Index}, Count: {Count}");
            Schedule(Send, TimeSpan.FromSeconds(Gamma.Sample(DefaultRS, Config.DelayTime_Expected, Config.DelayTime_CV)));
        }

        public event Action OnSend = () => { };
    }
}

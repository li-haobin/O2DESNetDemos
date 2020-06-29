using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo4
{
    public class PingPongGame : Sandbox
    {
        #region Statics
        public class Statics
        {
            public PingPongPlayer.Statics Player1 { get; set; }
            public PingPongPlayer.Statics Player2 { get; set; }
        }
        public Statics Config { get; private set; }
        #endregion

        #region Dynamics
        public PingPongPlayer Player1 { get; private set; }
        public PingPongPlayer Player2 { get; private set; }
        #endregion

        public PingPongGame(Statics config, int seed = 0) : base(seed)
        {
            Config = config;
            Player1 = AddChild(new PingPongPlayer(Config.Player1, DefaultRS.Next()));
            Player2 = AddChild(new PingPongPlayer(Config.Player2, DefaultRS.Next()));
            Player1.OnSend += Player2.Receive;
            Player2.OnSend += Player1.Receive;
            Schedule(Player1.Receive);
        }        
    }
}

using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo6
{
    /// <summary>
    /// M/M/c Queue in pushing mode
    /// </summary>
    public class MMcQueue_Push : Sandbox
    {
        #region Statics
        public class Statics
        {
            public int Capacity { get; set; }
            public double HourlyArrivalRate { get; set; }
            public double HourlyServiceRate { get; set; }
        }
        public Statics Config { get; private set; }
        #endregion

        #region Dynamics
        public Generator Generator { get; private set; }
        public Queue Queue { get; private set; }
        public Server Server { get; private set; }
        #endregion

        public MMcQueue_Push(Statics config, int seed = 0) : base(seed)
        {
            Config = config;
            Generator = AddChild(new Generator(Config.HourlyArrivalRate, DefaultRS.Next()));
            Queue = AddChild(new Queue());
            Server = AddChild(new Server(Config.Capacity, Config.HourlyServiceRate, DefaultRS.Next()));
            Generator.OnGenerate += Queue.Enqueue;
            Queue.OnDequeue += Server.Start;
            Server.OnChangeAccessibility += Queue.UpdateToDequeue;
        }        
    }
}

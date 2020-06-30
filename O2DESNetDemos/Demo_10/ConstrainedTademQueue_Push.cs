using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_10
{
    /// <summary>
    /// M/M/c Queue in pushing mode
    /// </summary>
    public class ConstrainedTademQueue_Push : Sandbox
    {
        #region Statics
        public class Statics
        {
            public int QueueCapacity { get; set; }
            public int ServerCapacity { get; set; }
            public double HourlyArrivalRate { get; set; }
            public double HourlyServiceRate { get; set; }
        }
        public Statics Config { get; private set; }
        #endregion

        #region Dynamics
        public Generator Generator { get; private set; }
        public Queue Queue1 { get; private set; }
        public Server Server1 { get; private set; }
        public Queue Queue2 { get; private set; }
        public Server Server2 { get; private set; }
        #endregion

        public ConstrainedTademQueue_Push(Statics config, int seed = 0) : base(seed)
        {
            Config = config;
            Generator = AddChild(new Generator(Config.HourlyArrivalRate, DefaultRS.Next()));
            Queue1 = AddChild(new Queue(Config.QueueCapacity));
            Server1 = AddChild(new Server(Config.ServerCapacity, Config.HourlyServiceRate, DefaultRS.Next()));
            Queue2 = AddChild(new Queue(Config.QueueCapacity));
            Server2 = AddChild(new Server(Config.ServerCapacity, Config.HourlyServiceRate, DefaultRS.Next()));
            /// Conenction for 1st Queue & Server
            Generator.OnGenerate += Queue1.Enqueue;
            Queue1.OnDequeue += Server1.Start;
            Server1.OnChangeAccessibility += Queue1.UpdateToDequeue;
            /// Connection for 2nd Queue & Server
            Server1.OnDepart += Queue2.Enqueue;
            Queue2.OnDequeue += Server2.Start;
            Queue2.OnChangeAccessibility += Server1.UpdateToDepart;
            Server2.OnChangeAccessibility += Queue2.UpdateToDequeue;
        }        
    }
}

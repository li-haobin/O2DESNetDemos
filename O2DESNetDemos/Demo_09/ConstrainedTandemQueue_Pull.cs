using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_09
{
    /// <summary>
    /// M/M/c Queue in pulling mode
    /// </summary>
    public class ConstrainedTandemQueue_Pull : Sandbox
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

        public ConstrainedTandemQueue_Pull(Statics config, int seed = 0) : base(seed)
        {
            Config = config;
            Generator = AddChild(new Generator(Config.HourlyArrivalRate, DefaultRS.Next()));
            Queue1 = AddChild(new Queue(Config.QueueCapacity));
            Server1 = AddChild(new Server(Config.ServerCapacity, Config.HourlyServiceRate, DefaultRS.Next()));
            Queue2 = AddChild(new Queue(Config.QueueCapacity));
            Server2 = AddChild(new Server(Config.ServerCapacity, Config.HourlyServiceRate, DefaultRS.Next()));
            /// Connect for 1st Queue & Server
            Generator.OnGenerate += Queue1.RequestToEnqueue;
            Generator.OnGenerate += Server1.RequestToStart;
            Server1.OnStart += Queue1.Dequeue;
            /// Connect for 2nd Queue & Server
            Server1.OnReadyToFinish += Queue2.RequestToEnqueue;
            Server1.OnReadyToFinish += Server2.RequestToStart;
            Queue2.OnEnqueue += Server1.Finish;
            Server2.OnStart += Queue2.Dequeue;
            /// Enclose 2nd Server
            Server2.OnReadyToFinish += Server2.Finish;
        }        
    }
}

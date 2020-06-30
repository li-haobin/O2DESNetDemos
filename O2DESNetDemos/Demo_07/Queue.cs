using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_07
{
    public class Queue : Sandbox
    {
        #region Statics
        #endregion

        #region Dynamics
        public int NumberWaiting { get; private set; } = 0;
        #endregion

        public Queue(int seed = 0) : base(seed)
        {
        }

        public void Enqueue()
        {
            NumberWaiting++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tEnqueue. #Waiting: {NumberWaiting}");
        }

        public void Dequeue()
        {
            NumberWaiting--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tDequeue. #Waiting: {NumberWaiting}");
        }
    }
}

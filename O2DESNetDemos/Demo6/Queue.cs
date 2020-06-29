using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo6
{
    public class Queue : Sandbox
    {
        #region Statics
        #endregion

        #region Dynamics
        public int NumberWaiting { get; private set; } = 0;
        public bool AbleToDequeue { get; private set; } = true;
        #endregion

        public Queue(int seed = 0) : base(seed)
        {
        }

        public void Enqueue()
        {
            NumberWaiting++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}\tEnqueue. #Waiting: {NumberWaiting}");
            if (AbleToDequeue) Dequeue();
        }

        public void UpdateToDequeue(bool ableToDequeue)
        {
            AbleToDequeue = ableToDequeue;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}\tUpdateToDequeue. AbleToDequeue: {AbleToDequeue}");
            if (AbleToDequeue && NumberWaiting > 0) Dequeue();
        }

        public void Dequeue()
        {
            NumberWaiting--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}\tDequeue. #Waiting: {NumberWaiting}");
            OnDequeue.Invoke();
        }

        public event Action OnDequeue = () => { };
    }
}

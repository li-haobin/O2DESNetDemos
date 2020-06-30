using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_09
{
    public class Queue : Sandbox
    {
        #region Statics
        public int Capacity { get; private set; }
        #endregion

        #region Dynamics
        public int NumberWaiting { get; private set; } = 0;
        public int NumberPending { get; private set; } = 0;
        #endregion

        public Queue(int capacity, int seed = 0) : base(seed)
        {
            Capacity = capacity;
        }

        public void RequestToEnqueue()
        {
            NumberPending++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tRequestToEnqueue. #Pending: {NumberPending} #Waiting: {NumberWaiting}");
            if (NumberWaiting < Capacity) Enqueue();
        }

        void Enqueue()
        {
            NumberPending--;
            NumberWaiting++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tEnqueue. #Pending: {NumberPending} #Waiting: {NumberWaiting}");
            OnEnqueue.Invoke();
        }

        public void Dequeue()
        {
            NumberWaiting--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tDequeue. #Pending: {NumberPending} #Waiting: {NumberWaiting}");
            if (NumberPending > 0) Enqueue();
        }

        public event Action OnEnqueue = () => { };
    }
}

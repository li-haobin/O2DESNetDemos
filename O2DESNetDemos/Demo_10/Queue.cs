using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_10
{
    public class Queue : Sandbox
    {
        #region Statics
        public int Capacity { get; private set; }
        #endregion

        #region Dynamics
        public int NumberWaiting { get; private set; } = 0;
        public bool AbleToDequeue { get; private set; } = true;
        #endregion

        public Queue(int capacity, int seed = 0) : base(seed)
        {
            Capacity = capacity;
        }

        public void Enqueue()
        {
            NumberWaiting++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tEnqueue. #Waiting: {NumberWaiting}");
            Schedule(AttemptToDequeue);
            if (NumberWaiting == Capacity) ChangeAccessibility();
        }

        public void UpdateToDequeue(bool ableToDequeue)
        {
            AbleToDequeue = ableToDequeue;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tUpdateToDequeue. AbleToDequeue: {AbleToDequeue}");
            if (AbleToDequeue && NumberWaiting > 0) Dequeue();
        }

        void AttemptToDequeue()
        {
            if (AbleToDequeue && NumberWaiting > 0) Dequeue();
        }

        void Dequeue()
        {
            NumberWaiting--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tDequeue. #Waiting: {NumberWaiting}");
            if (NumberWaiting == Capacity - 1) ChangeAccessibility();
            OnDequeue.Invoke();
        }

        void ChangeAccessibility()
        {
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tChangeAssessibility. #Waiting: {NumberWaiting}");
            OnChangeAccessibility.Invoke(NumberWaiting < Capacity);
        }

        public event Action OnDequeue = () => { };
        public event Action<bool> OnChangeAccessibility = (accessible) => { };
    }
}

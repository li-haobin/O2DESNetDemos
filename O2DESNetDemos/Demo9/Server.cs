using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo9
{
    public class Server : Sandbox
    {
        #region Statics
        public int Capacity { get; private set; }
        public double HourlyServiceRate { get; private set; }
        #endregion

        #region Dynamics
        public int NumberPending { get; private set; } = 0;
        public int NumberInService { get; private set; } = 0;
        #endregion

        public Server(int capacity, double hourlyServiceRate, int seed = 0) : base(seed)
        {
            Capacity = capacity;
            HourlyServiceRate = hourlyServiceRate;
        }

        public void RequestToStart()
        {
            NumberPending++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tRequestToStart. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            if (NumberInService < Capacity) Start();
        }

        void Start()
        {
            NumberPending--;
            NumberInService++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tStart. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            Schedule(ReadyToFinish, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyServiceRate)));
            OnStart.Invoke();
        }

        void ReadyToFinish()
        {
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tReadyToFinish. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            OnReadyToFinish.Invoke();
        }

        public void Finish()
        {
            NumberInService--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tFinish. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            if (NumberPending > 0) Start();
        }

        public event Action OnStart = () => { };
        public event Action OnReadyToFinish = () => { };
    }
}

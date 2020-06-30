using O2DESNet;
using O2DESNet.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace O2DESNetDemos.Demo_05
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
            Console.WriteLine($"{ClockTime}\t{GetType().Name}\tRequestToStart. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            if (NumberInService < Capacity) Start();
        }

        void Start()
        {
            NumberPending--;
            NumberInService++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}\tStart. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            Schedule(Finish, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyServiceRate)));
            OnStart.Invoke();
        }

        void Finish()
        {
            NumberInService--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}\tFinish. #Pending: {NumberPending}, #In-Service: {NumberInService}");
            if (NumberPending > 0) Start();
        }

        public event Action OnStart = () => { };
    }
}

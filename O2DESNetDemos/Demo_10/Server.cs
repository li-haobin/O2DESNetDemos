using O2DESNet;
using O2DESNet.Distributions;
using System;

namespace O2DESNetDemos.Demo_10
{
    public class Server : Sandbox
    {
        #region Statics
        public int Capacity { get; private set; }
        public double HourlyServiceRate { get; private set; }
        #endregion

        #region Dynamics
        public int NumberInService { get; private set; } = 0;
        public int NumberPendingDepart { get; private set; } = 0;
        public bool AbleToDepart { get; private set; } = true;
        #endregion

        public Server(int capacity, double hourlyServiceRate, int seed = 0) : base(seed)
        {
            Capacity = capacity;
            HourlyServiceRate = hourlyServiceRate;
        }

        public void Start()
        {
            if (NumberInService + NumberPendingDepart >= Capacity) throw new Exception("Insufficient vacancy.");
            NumberInService++;
            Schedule(Finish, TimeSpan.FromHours(Exponential.Sample(DefaultRS, 1 / HourlyServiceRate)));
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tStart. #In-Service: {NumberInService} #Pending-Depart: {NumberPendingDepart}");
            if (NumberInService == Capacity) ChangeAccessibility();
        }

        void Finish()
        {
            if (ClockTime >= new DateTime(1, 1, 1, 4, 12, 34)) ;
            NumberInService--;
            NumberPendingDepart++;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tFinish. #In-Service: {NumberInService} #Pending-Depart: {NumberPendingDepart}");
            if (AbleToDepart) Depart();
        }

        void Depart()
        {
            NumberPendingDepart--;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tDepart. #In-Service: {NumberInService} #Pending-Depart: {NumberPendingDepart}");
            if (NumberInService + NumberPendingDepart == Capacity - 1) ChangeAccessibility();
            OnDepart.Invoke();
        }

        public void UpdateToDepart(bool ableToDepart)
        {
            AbleToDepart = ableToDepart;
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tUpdateToDequeue. AbleToDepart: {AbleToDepart}");
            if (AbleToDepart && NumberPendingDepart > 0) Depart();
        }

        void ChangeAccessibility()
        {
            Console.WriteLine($"{ClockTime}\t{GetType().Name}#{Index}\tChangeAssessibility. #In-Service: {NumberInService}");
            OnChangeAccessibility.Invoke(NumberInService + NumberPendingDepart < Capacity);            
        }

        public event Action<bool> OnChangeAccessibility = (accessible) => { };
        public event Action OnDepart = () => { };
    }
}

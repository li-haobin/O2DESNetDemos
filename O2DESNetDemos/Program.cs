using System;

namespace O2DESNetDemos
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo8.Program.Run();

            /// next to be inlcuded
            /// 1. Constrained Tandem queue in both pushing and pulling mode
            /// 2. Multiple Queues (xN) in both pushing and pulling mode (unconstrained server, unconstrained queue)
            /// 3. Multiple Queues (xN) in both pushing and pulling mode (constrained server, unconstrained queue)
            /// 4. Multiple Queues (xN) in both pushing and pulling mode (constrained server, constrained queue)
            /// 5. Two echelon branching (pulling mode)
            /// 6. Multiple echelon branching (pulling mode)
        }
    }
}

using System;
using System.Threading;

namespace spp
{
    class Program
    {
        static ManualResetEvent suspend = new ManualResetEvent(true);
        static void Main(string[] args)
        {
            
            var n = int.Parse(Console.ReadLine());
            
            var thread = new Thread(Calculate);
            thread.Start(n);
            bool isWorking = true;
            while (true)
            {
                switch(Console.ReadLine()) {
                    case "q":
                        thread.Interrupt();
                        break;
                    case "p":
                        if (isWorking) {
                            suspend.Reset();
                        }
                        else {
                            suspend.Set();
                        }
                        isWorking = !isWorking;
                        break;
                }
                

            }
        }

        static void Calculate(object obj) {
            if(obj is int n) {
                double sum = 0.0;
                for (int i = 0; i < n; i++) {
                    suspend.WaitOne(Timeout.Infinite);

                    sum += 1 / factorial(i);
                    System.Console.WriteLine(sum);
                    Thread.Sleep(1500);
                }
            }
        }

        static double factorial(int n) {
            if (n == 0 || n == 1)
                return 1;
            return n <=2 ? n : n * factorial(n - 1);
        }
    }
}

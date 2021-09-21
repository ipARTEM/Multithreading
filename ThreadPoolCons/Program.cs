using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolCons
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread(Work);
                thread.Start(i);

                Thread.Sleep(200);

            }
            Console.WriteLine("*******************");
            Console.ReadLine();

            for (int i = 1; i <= 10; i++)
            {
                ThreadPool.QueueUserWorkItem(Work, i);

                Thread.Sleep(200);

            }
            Console.WriteLine("*******************");
            Console.ReadLine();
        }

        private static void Work(object i)
        {
            Console.WriteLine($" Идентификатор потока: {Thread.CurrentThread.ManagedThreadId}, параметр: {i}");
        }
    }
}

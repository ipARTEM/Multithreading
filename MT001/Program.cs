using System;
using System.Threading;

namespace MT001
{
    class Program
    {
        static int x = 0;

        static object locker = new object();

        static AutoResetEvent waitHandler = new AutoResetEvent(true);

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }

            Console.ReadLine();

            Console.WriteLine("************* 2 ************");

            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count2);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }

            Console.ReadLine();

            Console.WriteLine("************ 3 *************");

            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count3);
                myThread.Name = $"Поток {i.ToString()}";
                myThread.Start();
            }

            Console.ReadLine();

            Console.WriteLine("*********** 4 **************");

            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count4);
                myThread.Name = $"Поток {i.ToString()}";
                myThread.Start();
            }

            Console.ReadLine();

        }
        public static void Count()
        {

            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                x++;
                Thread.Sleep(100);
            }



        }

        public static void Count2()
        {
            lock (locker)
            {
                x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, x);
                    x++;
                    Thread.Sleep(100);
                }
            }


        }

        public static void Count3()
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(locker, ref acquiredLock);
                x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                if (acquiredLock) Monitor.Exit(locker);
            }
        }

        public static void Count4()
        {
            //waitHandler.WaitOne();
            //или
            AutoResetEvent.WaitAll(new WaitHandle[] { waitHandler });
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            waitHandler.Set();
        }
    }
}
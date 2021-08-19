using System;
using System.Threading;

namespace MT002
{
    class Program
    {
        static Mutex mutexObj = new Mutex();
        static int x = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("*********** Мьютекс ***********");

            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }

            Console.ReadLine();

            Console.WriteLine("*********** Cемафор ***********");

            for (int i = 1; i < 6; i++)
            {
                Reader reader = new Reader(i);
            }

            Console.ReadLine();

            Console.WriteLine("*********** Таймер ***********");
            int num = 0;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Count3);
            // создаем таймер
            Timer timer = new Timer(tm, num, 0, 2000);

            Console.ReadLine();
        }
        public static void Count()
        {
            mutexObj.WaitOne();
            x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            mutexObj.ReleaseMutex();
        }

        public static void Count3(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
            }
        }

        class Reader
        {
            // создаем семафор
            static Semaphore sem = new Semaphore(3, 3);
            Thread myThread;
            int count = 3;// счетчик чтения

            public Reader(int i)
            {
                myThread = new Thread(Read);
                myThread.Name = $"Читатель {i.ToString()}";
                myThread.Start();
            }

            public void Read()
            {
                while (count > 0)
                {
                    sem.WaitOne();

                    Console.WriteLine($"{Thread.CurrentThread.Name} входит в библиотеку");

                    Console.WriteLine($"{Thread.CurrentThread.Name} читает");
                    Thread.Sleep(1000);

                    Console.WriteLine($"{Thread.CurrentThread.Name} покидает библиотеку");

                    sem.Release();

                    count--;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
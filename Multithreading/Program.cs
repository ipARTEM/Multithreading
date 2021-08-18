using System;
using System.Threading;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            // получаем текущий поток
            Thread t = Thread.CurrentThread;

            //получаем имя потока
            Console.WriteLine($"Имя потока: {t.Name}");
            t.Name = "Метод Main";
            Console.WriteLine($"Имя потока: {t.Name}");

            Console.WriteLine($"Запущен ли поток: {t.IsAlive}");
            Console.WriteLine($"Приоритет потока: {t.Priority}");
            Console.WriteLine($"Статус потока: {t.ThreadState}");

            // получаем домен приложения
            Console.WriteLine($"Домен приложения: {Thread.GetDomain().FriendlyName}");

            Console.ReadLine();

            // создаем новый поток
            Thread myThread = new Thread(new ThreadStart(Count));
            myThread.Start(); // запускаем поток

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(300);
            }

            Console.ReadLine();
            //*************************************

            int number = 4;
            // создаем новый поток
            Thread myThread2 = new Thread(new ParameterizedThreadStart(Count2));
            myThread2.Start(number);

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(300);
            }

            Console.ReadLine();

            //*******************************

            Counter counter = new Counter();
            counter.x = 4;
            counter.y = 5;

            Thread myThread3 = new Thread(new ParameterizedThreadStart(Count3));
            myThread3.Start(counter);

            //*********************
            Console.ReadKey();
            Counter4 counter4 = new Counter4(5, 4);

            Thread myThread4 = new Thread(new ThreadStart(counter4.Count4));
            myThread4.Start();

        }

        public static void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(400);
            }
        }

        public static void Count2(object x)
        {
            for (int i = 1; i < 9; i++)
            {
                int n = (int)x;

                Console.WriteLine("Второй поток 2:");
                Console.WriteLine(i * n);
                Thread.Sleep(400);
            }
        }

        public static void Count3(object obj)
        {
            for (int i = 1; i < 9; i++)
            {
                Counter c = (Counter)obj;

                Console.WriteLine("Второй поток 3:");
                Console.WriteLine(i * c.x * c.y);
            }
        }
    }

    public class Counter
    {
        public int x;
        public int y;
    }

    public class Counter4
    {
        private int x;
        private int y;

        public Counter4(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
        }

        public void Count4()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток 4:");
                Console.WriteLine(i * x * y);
                Thread.Sleep(400);
            }
        }
    }
}

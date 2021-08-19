using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL005
{
    class Program
    {
        static void Main(string[] args)
        {

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            int number = 6;

            Task task1 = new Task(() =>
            {
                int result = 1;
                for (int i = 1; i <= number; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция прервана");
                        return;
                    }

                    result *= i;
                    Console.WriteLine($"Факториал числа {number} равен {result}");
                    Thread.Sleep(5000);
                }
            });
            task1.Start();

            Console.WriteLine("Введите Y для отмены операции или другой символ для ее продолжения:");
            string s = Console.ReadLine();
            if (s == "Y")
                cancelTokenSource.Cancel();

            Console.Read();

            Console.WriteLine("******************************");

            CancellationTokenSource cancelTokenSource2 = new CancellationTokenSource();
            CancellationToken token2 = cancelTokenSource.Token;

            Task task12 = new Task(() => Factorial2(5, token));
            task12.Start();

            Console.WriteLine("Введите Y для отмены операции или любой другой символ для ее продолжения:");
            string s2 = Console.ReadLine();
            if (s2 == "Y")
                cancelTokenSource.Cancel();

            Console.ReadLine();

            Console.WriteLine("******************************");

            CancellationTokenSource cancelTokenSource3 = new CancellationTokenSource();
            CancellationToken token3 = cancelTokenSource.Token;

            new Task(() =>
            {
                Thread.Sleep(400);
                cancelTokenSource.Cancel();
            }).Start();

            try
            {
                Parallel.ForEach<int>(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 },
                                        new ParallelOptions { CancellationToken = token }, Factorial3);
                // или так
                //Parallel.For(1, 8, new ParallelOptions { CancellationToken = token }, Factorial);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine("Операция прервана");
            }
            finally
            {
                cancelTokenSource.Dispose();
            }

            Console.ReadLine();


        }

        static void Factorial2(int x, CancellationToken token)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                result *= i;
                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(5000);
            }
        }

        static void Factorial3(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Факториал числа {x} равен {result}");
            Thread.Sleep(3000);
        }
    }
}

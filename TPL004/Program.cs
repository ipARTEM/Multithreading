using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL004
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke(Display,
          () =>
          {
              Console.WriteLine($"Выполняется задача 1 - {Task.CurrentId}");
              Thread.Sleep(3000);
              Console.WriteLine("1");
          },
          () => Factorial(5));

            Console.ReadLine();

            Console.WriteLine("*********************");

            Parallel.For(1, 10, Factorial2);

            Console.ReadLine();

            Console.WriteLine("*************************");

            ParallelLoopResult result = Parallel.ForEach<int>(new List<int>() { 1, 3, 5, 8 },
                Factorial3);

            Console.ReadLine();

            Console.WriteLine("***** Выход из цикла *****");

            ParallelLoopResult result5 = Parallel.For(1, 10, Factorial5);

            if (!result.IsCompleted)
                Console.WriteLine($"Выполнение цикла завершено на итерации {result.LowestBreakIteration}");
            Console.ReadLine();
        }

        static void Display()
        {
            Console.WriteLine($"Выполняется задача 2 - {Task.CurrentId}");
            Thread.Sleep(3000);
            Console.WriteLine("2");
        }

        static void Factorial5(int x, ParallelLoopState pls)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
                if (i == 5)
                    pls.Break();
            }
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Console.WriteLine($"Факториал числа {x} равен {result}");
        }

        static void Factorial3(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Console.WriteLine($"Факториал числа {x} равен {result}");
            Thread.Sleep(3000);
        }

        static void Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Выполняется задача 3 - {Task.CurrentId}");
            Thread.Sleep(3000);
            Console.WriteLine($"Результат {result}");
            Console.WriteLine("3");
        }

        static void Factorial2(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Console.WriteLine($"Факториал числа {x} равен {result}");
            Thread.Sleep(3000);
        }

    }
}
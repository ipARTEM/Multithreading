using System;
using System.Threading;
using System.Threading.Tasks;

namespace TPL002
{
    class Program
    {
        static void Main(string[] args)
        {
            var outer = Task.Factory.StartNew(() =>      // внешняя задача
            {
                Console.WriteLine("Outer task starting...");
                var inner = Task.Factory.StartNew(() =>  // вложенная задача
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                });
            });
            outer.Wait(); // ожидаем выполнения внешней задачи
            Console.WriteLine("End of Main");

            Console.ReadLine();

            Console.WriteLine("******************");

            var outer2 = Task.Factory.StartNew(() =>      // внешняя задача
            {
                Console.WriteLine("Outer task starting...");
                var inner2 = Task.Factory.StartNew(() =>  // вложенная задача
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                }, TaskCreationOptions.AttachedToParent);
            });
            outer2.Wait(); // ожидаем выполнения внешней задачи
            Console.WriteLine("End of Main");

            Console.ReadLine();

            Console.WriteLine("******************");

            Task[] tasks1 = new Task[3]
                {
                    new Task(() => Console.WriteLine("First Task")),
                    new Task(() => Console.WriteLine("Second Task")),
                    new Task(() => Console.WriteLine("Third Task"))
                };
            // запуск задач в массиве
            foreach (var t in tasks1)
                t.Start();

            Console.WriteLine("******************");

            Task[] tasks2 = new Task[3];
            int j = 1;
            for (int i = 0; i < tasks2.Length; i++)
                tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));

            Console.WriteLine("******************");

            Task[] tasks3 = new Task[3]
                {
                    new Task(() => Console.WriteLine("First Task")),
                    new Task(() => Console.WriteLine("Second Task")),
                    new Task(() => Console.WriteLine("Third Task"))
                };
                        foreach (var t3 in tasks3)
                t3.Start();

            Task[] tasks4 = new Task[3];

            int j2 = 1;
            for (int i = 0; i < tasks3.Length; i++)
                tasks3[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j2++}"));

            Console.WriteLine("Завершение метода Main");

            Console.ReadLine();

            Console.WriteLine("******** ожидаем завершение всех задач из массива **********");

            Task[] tasks5 = new Task[3]
    {
        new Task(() => Console.WriteLine("First Task 5")),
        new Task(() => Console.WriteLine("Second Task 5")),
        new Task(() => Console.WriteLine("Third Task 5"))
    };
            foreach (var t in tasks5)
                t.Start();
            Task.WaitAll(tasks5); // ожидаем завершения задач 

            Console.WriteLine("Завершение метода Main");

            Console.ReadLine();

        }
    }
}

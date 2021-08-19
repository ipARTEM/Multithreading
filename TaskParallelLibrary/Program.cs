using System;
using System.Threading.Tasks;

namespace TaskParallelLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = new Task(() => Console.WriteLine("Hello Task!"));
            task.Start();


            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Hello Task 2!"));

            Task task11 = new Task(() => Console.WriteLine("Task11 is executed"));
            task11.Start();

            Task task22 = Task.Factory.StartNew(() => Console.WriteLine("Task22 is executed"));

            Task task33 = Task.Run(() => Console.WriteLine("Task33 is executed"));

            Console.ReadLine();

            Console.WriteLine("***********************");

            Task task4 = new Task(Display);
            task4.Start();
            task4.Wait();   // ждать завершение работы вторичного потока

            Console.WriteLine("Завершение метода Main");

            Console.ReadLine();
        }

        static void Display()
        {
            Console.WriteLine("Начало работы метода Display");

            Console.WriteLine("Завершение работы метода Display");
        }
    }
}

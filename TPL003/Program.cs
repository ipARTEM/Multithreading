using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL003
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task1 = new Task<int>(() => Factorial(5));
            task1.Start();

            Console.WriteLine($"Факториал числа 5 равен {task1.Result}");

            Task<Book> task2 = new Task<Book>(() =>
            {
                return new Book { Title = "Война и мир", Author = "Л. Толстой" };
            });
            task2.Start();

            Book b = task2.Result;  // ожидаем получение результата
            Console.WriteLine($"Название книги: {b.Title}, автор: {b.Author}");

            Console.ReadLine();

            Console.WriteLine("******************");

            Task task3 = new Task(() => {
                Console.WriteLine($"Id задачи: {Task.CurrentId}");
            });

            // задача продолжения
            Task task4 = task3.ContinueWith(Display2);

            task3.Start();

            // ждем окончания четвёртой задачи
            task4.Wait();
            Console.WriteLine("Выполняется работа метода Main");
            Console.ReadLine();

            Console.WriteLine("******************");

            Task<int> task5 = new Task<int>(() => Sum(4, 5));

            // задача продолжения
            Task task6 = task1.ContinueWith(sum => Display5(sum.Result));

            task5.Start();

            // ждем окончания шестой задачи
            task6.Wait();
            Console.WriteLine("End of Main");
            Console.ReadLine();

            Console.WriteLine("******************");

            Task task11 = new Task(() => {
                Console.WriteLine($"Id задачи: {Task.CurrentId}");
            });

            // задача продолжения
            Task task12 = task11.ContinueWith(DisplayX);

            Task task13 = task11.ContinueWith((Task t) =>
            {
                Console.WriteLine($"Id задачи: {Task.CurrentId}");
            });

            Task task14 = task12.ContinueWith((Task t) =>
            {
                Console.WriteLine($"Id задачи: {Task.CurrentId}");
            });

            task11.Start();

            Console.ReadLine();
        }

        static void DisplayX(Task t)
        {
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
        }

        static int Sum(int a, int b) => a + b;
        static void Display5(int sum)
        {
            Console.WriteLine($"Sum: {sum}");
        }


        static int Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }

            return result;
        }

        static void Display2(Task t)
        {
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
            Console.WriteLine($"Id предыдущей задачи: {t.Id}");
            Thread.Sleep(3000);
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }

    
}

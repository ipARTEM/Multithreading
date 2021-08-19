using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {

        static void Secondary()
        {
            while (true)
            {
                Console.WriteLine(new string(' ', 20) + "Вторичный поток");
            }
        }



        static void Main(string[] args)
        {

            ThreadStart threadTwo = new ThreadStart(Secondary);

            Thread thread = new Thread(threadTwo);
            thread.Start();



            while (true)
            {
                Console.WriteLine("Первичный поток");
            }
        }
    }
}

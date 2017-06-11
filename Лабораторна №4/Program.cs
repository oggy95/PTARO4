using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Лаб2
{
    class Program
    {
        static int number = 20;
        static int iterator = 1;
        static int[] Arr;
        static int i, j, result, min1, m, n;
        static int[,] mas = new int[number, number];
        static Random rnd = new Random();

        public class EntryPoint
        {
            public static void TimerProc(object state)
            { }
        }

        static void Main()
        {
            int workerThreads;
            int portThreads;

            for (i = 0; i < number; i++)
                for (j = 0; j < number; j++)
                    mas[i, j] = rnd.Next(-30, 30);

            Console.WriteLine("Processor=" + Environment.ProcessorCount);
            ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
            Console.WriteLine("\nMaximum worker threads: \t{0}" + "\nMaximum completion port threads: {1}", workerThreads, portThreads);
            int MaxThreadsCount = 6;
            ThreadPool.SetMaxThreads(MaxThreadsCount, MaxThreadsCount);
            ThreadPool.SetMinThreads(2, 2);

            //for (i = 0; i < number; i++)
            //{
            //    for (j = 0; j < number; j++)
            //        Console.Write(mas[i, j] + " ");
            //    Console.WriteLine();
            //}


            Console.WriteLine("start time=" + DateTime.Now.Millisecond);

            for (i = 0; i < number; i++)
                for (j = 0; j < number; j++)
                {
                    if (i + j == number - 1)
                    {
                        Console.Write(mas[i, j] + " ");
                        Arr = new int[2] { i, mas[i, j] };
                        ThreadPool.QueueUserWorkItem(Function, Arr);
                    }
                }

            ThreadPool.GetMaxThreads(out workerThreads, out portThreads);
            while (iterator != number) ;
            Console.WriteLine("\nMaximum worker threads: \t{0}" + "\nMaximum completion port threads:{1}", workerThreads, portThreads);
            Console.WriteLine("result=" + min1 + " i=" + n);
            Console.WriteLine("end time=" + DateTime.Now.Millisecond);
            Console.ReadLine();
        }

        public static void Function(object instance)
        {
            m++;
            int[] myArr = (int[])instance;
            myArr[0]++;
            if (myArr[1] < min1)
            {
                n++;
                min1 = myArr[1];
                n = myArr[0];
            }
            Thread.Sleep(500);
            Console.WriteLine("min1=" + min1 + " i=" + n);
            iterator++;

        }
    }
}
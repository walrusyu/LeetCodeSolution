using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();

            var result = solution.MyPow(0.00001, 2147483647);
            //var result = solution.MyPow(2.0, 10);

            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void PrintSomething(int i)
        {
            Console.WriteLine(i);
        }
    }
}

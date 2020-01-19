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
            var result = solution.Divide(-2147483648, -1);
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void PrintSomething(int i)
        {
            Console.WriteLine(i);
        }
    }
}

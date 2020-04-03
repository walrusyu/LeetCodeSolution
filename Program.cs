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
            var grid = new char[2][] { new char[1] { 'A' }, new char[1] { 'A' } };

            //solution.SetZeroes(grid);
            var result = solution.Exist(grid, "AA");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void PrintSomething(int i)
        {
            Console.WriteLine(i);
        }
    }
}

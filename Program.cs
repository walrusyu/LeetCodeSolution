using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            var result = solution.LetterCombinations("123");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void PrintSomething(int i)
        {
            Console.WriteLine(i);
        }
    }
}

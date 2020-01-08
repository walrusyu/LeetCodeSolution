using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            var result = solution.ThreeSumClosest(new int[] { -55,-24,-18,-11,-7,-3,4,5,6,9,11,23,33 }, 0);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}

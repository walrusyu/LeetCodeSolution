using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();

            // int[] height = new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 };
            // var result = solution.MaxArea(height);

            //var result = solution.IntToRoman(2341);

            // var result = solution.RomanToInt("MDCXCV");

            // var result = solution.LongestCommonPrefix(new string[]{"flower","flow","flight"});

            // var result = solution.ThreeSum(new int[]{-2, 0, 1, 1, 2});
            // var result = solution.ThreeSum(new int[] { -1, 0, 1, 2, -1, -4 });
            var result = solution.ThreeSum(new int[] { 1, -1, -1, 0 });
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}

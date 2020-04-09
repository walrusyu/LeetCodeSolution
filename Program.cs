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
            // var array = new int[] { 3, 14, 15, 9, 26, 53, 5, 8, 9, 79 };
            // var tree = TreeNode.GenerateTree(array);


            // var array = new int[] { 2, 1, 2 };
            var matrix = new char[][] { new char[] { '1', '0', '1', '0', '0' }, new char[] { '1', '0', '1', '1', '1' }, new char[] { '1', '1', '1', '1', '1' }, new char[] { '1', '0', '0', '1', '0' } };
            Solution solution = new Solution();


            var node = new ListNode(1);
            node.next = new ListNode(4);
            node.next.next = new ListNode(3);
            node.next.next.next = new ListNode(2);
            node.next.next.next.next = new ListNode(5);
            node.next.next.next.next.next = new ListNode(2);


            var result = solution.IsScramble("great", "rgeat");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static void PrintSomething(int i)
        {
            Console.WriteLine(i);
        }
    }
}

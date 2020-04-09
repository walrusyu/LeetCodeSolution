using System;

//存储最小值的线段树
public class TreeNode
{
    public int Start { get; set; }
    public int End { get; set; }
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }
    public int Value { get; set; }

    public static TreeNode GenerateTree(int[] numbers)
    {
        return GenerateTree(numbers, 0, numbers.Length - 1);
    }

    private static TreeNode GenerateTree(int[] numbers, int left, int right)
    {
        var root = new TreeNode { Start = left, End = right };
        if (left < right)
        {
            root.Left = GenerateTree(numbers, left, (left + right) / 2);
            root.Right = GenerateTree(numbers, (left + right) / 2 + 1, right);
            root.Value = Math.Min(root.Left.Value, root.Right.Value);
        }
        else
        {
            root.Value = numbers[left];
        }

        return root;
    }
}

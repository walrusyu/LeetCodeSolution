using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Solution
{
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (map.ContainsKey(nums[i]))
            {
                if (nums[i] * 2 == target)
                {
                    return new int[] { map[nums[i]], i };
                }
            }
            else
            {
                map.Add(nums[i], i);
            }
        }

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            if (map.ContainsKey(complement) && map[complement] != i)
            {
                return new int[] { i, map[complement] };
            }
        }
        return null;
    }

    public int MaxArea(int[] height)
    {
        var maxArea = 0;
        if (height.Length <= 1)
        {
            return maxArea;
        }

        var l = 0;
        var r = height.Length - 1;
        while (l < r)
        {
            if (height[l] < height[r])
            {
                maxArea = Math.Max((r - l) * height[l], maxArea);
                l++;
            }
            else
            {
                maxArea = Math.Max((r - l) * height[r], maxArea);
                r--;
            }
        }

        return maxArea;
    }

    public string IntToRoman(int num)
    {
        string[] symbles = new string[] { "M", "D", "C", "L", "X", "V", "I" };

        if (num < 1 && num > 3999)
        {
            return string.Empty;
        }
        var index = 0;
        var divisor = 100;
        string result = string.Empty;

        while (num >= 1)
        {
            if (num >= 1000)
            {
                var a = num / 1000;
                num = num % 1000;
                while (a-- > 0)
                {
                    result += symbles[0];
                }
            }
            else
            {
                if (divisor > num)
                {
                    index += 2;
                    divisor /= 10;
                }
                else
                {
                    var a = num / divisor;
                    num = num % divisor;
                    result += ConvertToRoman(a, symbles[index], symbles[index + 1], symbles[index + 2]);
                }
            }
        }

        return result;
    }

    private string ConvertToRoman(int number, string ten, string five, string one)
    {
        if (number <= 0 || number > 9)
        {
            return string.Empty;
        }
        string result = string.Empty;
        while (number > 0)
        {
            if (number == 9)
            {
                result += one;
                result += ten;
                break;
            }
            else if (number >= 5)
            {
                result += five;
                number -= 5;
            }
            else if (number == 4)
            {
                result += one;
                result += five;
                break;
            }
            else
            {
                result += one;
                number--;
            }
        }
        return result;
    }

    public int RomanToInt(string s)
    {
        int[] array = new int[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            array[i] = GetValue(s[i]);
        }
        var result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (i == s.Length - 1)
            {
                result += array[i];
            }
            else
            {
                if (array[i] < array[i + 1])
                {
                    result += array[i + 1] - array[i];
                    i++;
                }
                else
                {
                    result += array[i];
                }
            }
        }
        return result;
    }

    private int GetValue(char ch)
    {
        switch (ch)
        {
            case 'I': return 1;
            case 'V': return 5;
            case 'X': return 10;
            case 'L': return 50;
            case 'C': return 100;
            case 'D': return 500;
            case 'M': return 1000;
            default: return 0;
        }
    }

    public string LongestCommonPrefix(string[] strs)
    {
        if (strs.Length == 0)
        {
            return string.Empty;
        }
        if (strs.Length == 1)
        {
            return strs[0];
        }
        var index = -1;
        var minLength = strs[0].Length;
        foreach (var str in strs)
        {
            minLength = Math.Min(minLength, str.Length);
        }

        for (int i = 0; i < minLength; i++)
        {
            var target = strs[0][i];
            var same = true;
            for (var j = 1; j < strs.Length; j++)
            {
                if (strs[j][i] != target)
                {
                    same = false;
                    break;
                }
            }
            if (same)
            {
                index = i;
            }
            else
            {
                break;
            }
        }

        if (index < 0)
        {
            return string.Empty;
        }
        return strs[0].Substring(0, index + 1);
    }

    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var result = new List<IList<int>>();
        if (nums.Length < 3)
        {
            return result;
        }

        Array.Sort(nums);

        for (int k = 0; k < nums.Length - 2; k++)
        {
            if (nums[k] > 0)
            {
                break;
            }
            //后一个值如果与前一个值相同，则该值可能包含的所有解，前一个值肯定已经获取过
            if (k > 0 && nums[k] == nums[k - 1])
            {
                continue;
            }

            int i = k + 1;
            int j = nums.Length - 1;
            int target = -nums[k];

            while (i < j)
            {
                //只有在命中目标时，才需要对后续值进行去重，未命中的不会录入结果，所以不需要做去重动作
                if (nums[i] + nums[j] == target)
                {
                    result.Add(new List<int> { nums[i], nums[j], nums[k] });
                    i++;
                    j--;
                    while (i < j && nums[i] == nums[i - 1])
                    {
                        i++;
                    }
                    while (i < j && nums[j] == nums[j + 1])
                    {
                        j--;
                    }
                }
                else if (nums[i] + nums[j] > target)
                {
                    j--;
                }
                else
                {
                    i++;
                }
            }
        }

        return result;
    }

    public int ThreeSumClosest(int[] nums, int target)
    {
        if (nums.Length < 3)
        {
            return 0;
        }

        Array.Sort(nums);
        var result = nums[0] + nums[1] + nums[2];

        for (int k = 0; k < nums.Length - 2; k++)
        {
            if (nums[k] > target / 3)
            {
                break;
            }

            var l = k + 1;
            var r = nums.Length - 1;
            var t = target - nums[k];
            var gap = int.MaxValue;
            while (l < r)
            {
                var newGap = 0;
                if (nums[l] + nums[r] == t)
                {
                    return target;
                }
                else if (nums[l] + nums[r] > t)
                {
                    newGap = nums[l] + nums[r] - t;
                    r--;
                }
                else
                {
                    newGap = nums[l] + nums[r] - t;
                    l++;
                }

                gap = Math.Abs(gap) > Math.Abs(newGap) ? newGap : gap;
            }
            if (Math.Abs(gap) < Math.Abs(result - target))
            {
                result = target + gap;
            }
        }
        return result;
    }

    public IList<string> LetterCombinations(string digits)
    {
        Dictionary<char, List<char>> dict = new Dictionary<char, List<char>>{
            {'2',new List<char>{'a','b','c'}},
            {'3',new List<char>{'d','e','f'}},
            {'4',new List<char>{'g','h','i'}},
            {'5',new List<char>{'j','k','l'}},
            {'6',new List<char>{'m','n','o'}},
            {'7',new List<char>{'p','q','r','s'}},
            {'8',new List<char>{'t','u','v'}},
            {'9',new List<char>{'w','x','y','z'}}
        };

        var root = new Node(' ');
        var currentLevel = new List<Node> { root };
        foreach (char digit in digits)
        {
            var nextLevel = new List<Node>();
            if (!dict.ContainsKey(digit))
                continue;
            foreach (var node in currentLevel)
            {
                foreach (var value in dict[digit])
                {
                    var newNode = new Node(value);
                    node.Children.Add(newNode);
                    nextLevel.Add(newNode);
                }
            }
            currentLevel = nextLevel;
        }

        List<string> result = new List<string>();
        var array = new char[digits.Length];
        foreach (var node in root.Children)
        {
            Calculate(result, array, 0, node);
        }

        return result;
    }

    public void Calculate(List<string> result, char[] array, int index, Node node)
    {
        array[index] = node.Value;
        if (node.Children.Count > 0)
        {
            index++;
            foreach (var child in node.Children)
            {
                Calculate(result, array, index, child);
            }
        }
        else
        {
            result.Add(string.Join("", array));
        }
    }
}

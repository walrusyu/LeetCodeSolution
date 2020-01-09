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

            int i = k + 1;
            int j = nums.Length - 1;
            if (nums[i] == nums[k])
            {
                while (i < j && nums[i] == nums[i + 1])
                {
                    i++;
                }
                k = i - 1;
                if (k > 0 && nums[k - 1] == nums[k] && nums[k] == 0)
                {
                    result.Add(new List<int> { 0, 0, 0 });
                }
                var temp = i + 1;
                while (temp <= j)
                {
                    if (nums[i] + nums[temp] + nums[k] == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[temp], nums[k] });
                        break;
                    }
                    else
                    {
                        temp++;
                    }
                }
                continue;
            }

            int target = -nums[k];

            while (i < j)
            {
                var iMoved = false;
                var jMoved = false;
                if (nums[i] + nums[j] == target)
                {
                    result.Add(new List<int> { nums[i], nums[j], nums[k] });
                    i++;
                    iMoved = true;
                    j--;
                    jMoved = true;
                }
                else if (nums[i] + nums[j] > target)
                {
                    j--;
                    jMoved = true;
                }
                else
                {
                    i++;
                    iMoved = true;
                }

                while (i < j)
                {
                    var equal = false;
                    if (iMoved && nums[i] == nums[i - 1])
                    {
                        i++;
                        equal = true;
                    }
                    if (jMoved && nums[j] == nums[j + 1])
                    {
                        j--;
                        equal = true;
                    }
                    if (!equal)
                    {
                        break;
                    }
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
}
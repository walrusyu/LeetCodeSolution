using System;
using System.Collections.Generic;
using System.Linq;

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

        List<string> result = new List<string>();
        var array = new char[digits.Length];
        Calculate(result, array, ref digits, -1, dict);

        return result;
    }

    public void Calculate(List<string> result, char[] array, ref string digits, int index, Dictionary<char, List<char>> dict)
    {
        index++;
        if (index >= digits.Length)
        {
            var value = string.Join("", array);
            if (!string.IsNullOrWhiteSpace(value))
            {
                result.Add(value);
            }
        }
        else
        {
            if (!dict.ContainsKey(digits[index]))
            {
                array[index] = (char)0x00;
                Calculate(result, array, ref digits, index, dict);
            }
            else
            {
                foreach (var value in dict[digits[index]])
                {
                    array[index] = value;
                    Calculate(result, array, ref digits, index, dict);
                }
            }
        }
    }

    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        Array.Sort(nums);
        var result = new List<IList<int>>();
        if (nums.Length < 4)
        {
            return result;
        }

        for (int i = 0; i < nums.Length - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            for (int j = i + 1; j < nums.Length - 2; j++)
            {
                if (j > i + 1 && nums[j] == nums[j - 1])
                {
                    continue;
                }

                int l = j + 1;
                int r = nums.Length - 1;
                while (l < r)
                {
                    var temp = target - (nums[i] + nums[j]);
                    if (nums[l] + nums[r] == temp)
                    {
                        result.Add(new List<int> { nums[i], nums[j], nums[l], nums[r] });
                        l++;
                        r--;
                        while (l < r && nums[r] == nums[r + 1])
                        {
                            r--;
                        }
                        while (l < r && nums[l] == nums[l - 1])
                        {
                            l++;
                        }
                    }
                    else if (nums[l] + nums[r] > temp)
                    {
                        r--;
                        while (l < r && nums[r] == nums[r + 1])
                        {
                            r--;
                        }
                    }
                    else
                    {
                        l++;
                        while (l < r && nums[l] == nums[l - 1])
                        {
                            l++;
                        }
                    }
                }
            }
        }

        return result;
    }

    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        var result = head;
        var target = head;
        var index = 0;
        var triggered = false;
        while (head.next != null)
        {
            if (triggered)
            {
                target = target.next;
            }
            head = head.next;
            index++;
            if (index == n)
            {
                triggered = true;
            }
        }
        if (!triggered)
        {
            result = result.next;
        }
        else
        {
            target.next = target.next.next;
        }
        return result;
    }

    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        var root = new ListNode(0);
        var current = root;
        while (l1 != null && l2 != null)
        {
            if (l1.val <= l2.val)
            {
                current.next = l1;
                current = current.next;
                l1 = l1.next;
            }
            else
            {
                current.next = l2;
                current = current.next;
                l2 = l2.next;
            }
        }
        if (l1 == null)
        {
            current.next = l2;
        }
        else
        {
            current.next = l1;
        }

        return root.next;
    }

    public IList<string> GenerateParenthesis(int n)
    {
        IList<string> result = new List<string>();
        char[] array = new char[2 * n];

        GenerateParenthesis(result, 0, 0, array);
        return result;
    }

    private void GenerateParenthesis(IList<string> result, int n, int count, char[] array)
    {
        if (n >= array.Length)
        {
            result.Add(string.Join("", array));
            return;
        }
        if (count >= array.Length / 2)
        {
            array[n] = ')';
            n++;
            GenerateParenthesis(result, n, count, array);
        }
        else
        {
            //左括号
            array[n] = '(';
            n++;
            count++;
            GenerateParenthesis(result, n, count, array);

            //右括号
            n--;
            count--;
            if (n <= count * 2 - 1)
            {
                array[n] = ')';
                n++;
                GenerateParenthesis(result, n, count, array);
            }
        }
    }

    public ListNode MergeKLists(ListNode[] lists)
    {
        if (lists.Length == 0)
        {
            return null;
        }
        var nodeList = lists.ToList();
        while (nodeList.Count > 1)
        {
            for (int i = 0; i < nodeList.Count;)
            {
                if (i + 1 < nodeList.Count)
                {
                    var temp = MergeTwoLists(nodeList[i], nodeList[i + 1]);
                    nodeList[i] = temp;
                    nodeList.RemoveAt(i + 1);
                }
                i++;
            }
        }
        return nodeList.First();
    }

    public ListNode SwapPairs(ListNode head)
    {
        if (head == null || head.next == null)
        {
            return head;
        }

        var next = head.next;
        head.next = SwapPairs(head.next.next);
        next.next = head;
        return next;
    }

    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null)
        {
            return null;
        }
        var tail = head;
        var count = 0;
        while (count < k - 1 && tail != null && tail.next != null)
        {
            var next = tail.next;
            count++;
            if (next == null)
            {
                break;
            }
            else
            {
                tail.next = next.next;
                next.next = head;
                head = next;
            }
        }
        if (count >= k - 1)
        {
            tail.next = ReverseKGroup(tail.next, k);
            return head;
        }
        else
        {
            return ReverseKGroup(head, count + 1);
        }
    }

    public int RemoveDuplicates(int[] nums)
    {
        var index = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            while (i + 1 < nums.Length && nums[i] == nums[i + 1])
            {
                i++;
            }
            nums[index] = nums[i];
            index++;
        }
        return index;
    }

    public int RemoveElement(int[] nums, int val)
    {
        var index = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                nums[index] = nums[i];
                index++;
            }
        }
        return index;
    }

    public int StrStr(string haystack, string needle)
    {
        var result = -1;
        for (int i = 0; i < haystack.Length - needle.Length + 1; i++)
        {
            var match = true;
            for (var j = 0; j < needle.Length; j++)
            {
                if (needle[j] != haystack[i + j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    public int Divide(int dividend, int divisor)
    {
        if (dividend == 0 || divisor == 0)
        {
            return 0;
        }
        if (divisor == 1)
        {
            return dividend;
        }
        if (divisor == -1)
        {
            return dividend == int.MinValue ? int.MaxValue : -dividend;
        }
        if (dividend < 0 && divisor < 0)
        {
            if (dividend > divisor)
            {
                return 0;
            }
            if (dividend == -1)
            {
                return 1;
            }
            var t1 = dividend >> 1;
            var t2 = divisor;
            var k = 1;
            while (t1 <= t2)
            {
                t2 = t2 << 1;
                k = k + k;
            }

            dividend = dividend - t2;
            return k + Divide(dividend, divisor);
        }
        else if (dividend > 0 && divisor > 0)
        {
            if (dividend < divisor)
            {
                return 0;
            }
            var t1 = dividend >> 1;
            var t2 = divisor;
            var k = 1;
            while (t1 >= t2)
            {
                t2 = t2 << 1;
                k = k + k;
            }

            dividend = dividend - t2;
            return k + Divide(dividend, divisor);
        }
        else if (dividend < 0 && divisor > 0)
        {
            return -Divide(dividend, -divisor);
        }
        else
        {
            return -Divide(-dividend, divisor);
        }
    }

    public IList<int> FindSubstring(string s, string[] words)
    {
        var result = new List<int>();
        if (words.Length == 0 || words[0].Length == 0)
        {
            return result;
        }
        var wordDict = new Dictionary<string, int>();
        foreach (var word in words)
        {
            if (wordDict.ContainsKey(word))
            {
                wordDict[word] = wordDict[word] + 1;
            }
            else
            {
                wordDict.Add(word, 1);
            }
        }

        var step = words[0].Length;
        var length = words.Length * words[0].Length;

        var verifyDict = new Dictionary<string, int>();
        for (int i = 0; i <= s.Length - length; i++)
        {
            var success = true;
            for (int j = i; j < i + length; j = j + step)
            {
                var word = s.Substring(j, step);
                if (wordDict.ContainsKey(word))
                {
                    if (verifyDict.ContainsKey(word))
                    {
                        verifyDict[word] = verifyDict[word] + 1;
                    }
                    else
                    {
                        verifyDict.Add(word, 1);
                    }

                    if (verifyDict[word] > wordDict[word])
                    {
                        success = false;
                        break;
                    }
                }
                else
                {
                    success = false;
                    break;
                }
            }
            verifyDict.Clear();
            if (success)
            {
                result.Add(i);
            }
        }
        return result;
    }

    public void NextPermutation(int[] nums)
    {
        var swapped = false;
        for (int i = nums.Length - 2; i >= 0; i--)
        {
            for (int j = nums.Length - 1; j > i; j--)
            {
                if (nums[i] < nums[j])
                {
                    swapped = true;
                    nums[i] = nums[i] + nums[j];
                    nums[j] = nums[i] - nums[j];
                    nums[i] = nums[i] - nums[j];

                    while (j < nums.Length - 1)
                    {
                        if (nums[j] >= nums[j + 1])
                        {
                            break;
                        }
                        else
                        {
                            j++;
                        }
                    }

                    //倒排
                    var length = nums.Length - (i + 1);
                    for (var offset = 0; offset < length / 2; offset++)
                    {
                        var l = i + 1 + offset;
                        var r = nums.Length - 1 - offset;
                        nums[l] = nums[l] + nums[r];
                        nums[r] = nums[l] - nums[r];
                        nums[l] = nums[l] - nums[r];
                    }

                    break;
                }
            }
            if (swapped)
            {
                break;
            }
        }

        if (!swapped)
        {
            for (var i = 0; i < nums.Length / 2; i++)
            {
                var j = nums.Length - 1 - i;
                nums[i] = nums[i] + nums[j];
                nums[j] = nums[i] - nums[j];
                nums[i] = nums[i] - nums[j];
            }
        }
    }
}
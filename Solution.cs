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

    public int LongestValidParentheses(string s)
    {
        var dp = new int[s.Length];
        int maxLength = 0;
        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] == ')')
            {
                if (s[i - 1] == '(')
                {
                    dp[i] = 2;
                    if (i - dp[i - 1] - 2 >= 0)
                    {
                        dp[i] += dp[i - dp[i - 1] - 2];
                    }
                }
                else
                {
                    if (i - dp[i - 1] - 1 >= 0 && s[i - dp[i - 1] - 1] == '(')
                    {
                        dp[i] = dp[i - 1] + 2;
                        if (i - dp[i - 1] - 2 >= 0)
                        {
                            dp[i] += dp[i - dp[i - 1] - 2];
                        }
                    }
                }
            }
            maxLength = Math.Max(maxLength, dp[i]);
        }

        return maxLength;
    }


    public int Search(int[] nums, int target)
    {
        var result = -1;
        if (nums.Length == 0)
        {
            return result;
        }

        var inLeft = target >= nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            if (target == nums[i])
            {
                result = i;
                break;
            }
            if (i == nums.Length - 1)
            {
                break;
            }
            if (nums[i] > nums[i + 1])
            {
                if (!inLeft && nums[i + 1] > target)
                {
                    break;
                }
            }
        }
        return result;
    }

    public int[] SearchRange(int[] nums, int target)
    {
        var notFoundResult = new int[] { -1, -1 };
        if (nums == null || nums.Length == 0)
        {
            return notFoundResult;
        }

        var result = FindPosition(nums, 0, nums.Length - 1, target);
        if (result >= 0)
        {
            var left = result;
            var right = result;
            for (int i = result; i >= 0; i--)
            {
                if (nums[i] != target)
                {
                    break;
                }
                else
                {
                    left = i;
                }
            }
            for (int i = result; i < nums.Length; i++)
            {
                if (nums[i] != target)
                {
                    break;
                }
                else
                {
                    right = i;
                }
            }
            return new int[] { left, right };
        }
        else
        {
            return notFoundResult;
        }

    }

    private int FindPosition(int[] nums, int start, int end, int target)
    {
        if (start > end)
        {
            return -1;
        }
        else if (start == end)
        {
            return target == nums[start] ? start : -1;
        }
        var center = (start + end) / 2;
        if (nums[center] == target)
        {
            return center;
        }
        else
        {
            var leftResult = FindPosition(nums, start, center - 1, target);

            if (leftResult >= 0)
            {
                return leftResult;
            }
            else
            {
                var rightResult = FindPosition(nums, center + 1, end, target);
                if (rightResult >= 0)
                {
                    return rightResult;
                }
                else
                {
                    return -1;
                }
            }
        }
    }

    public int SearchInsert(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return 0;
        }
        else if (nums[0] > target)
        {
            return 0;
        }
        else if (nums.Last() < target)
        {
            return nums.Length;
        }
        else
        {
            return FindPosition2(nums, 0, nums.Length, target);
        }
    }

    private int FindPosition2(int[] nums, int start, int end, int target)
    {
        if (start == end)
        {
            if (nums[start] >= target)
            {
                return start;
            }
            else
            {
                return start + 1;
            }
        }
        else if (start == end - 1)
        {
            if (nums[start] == target)
            {
                return start;
            }
            else
            {
                return end;
            }
        }
        else
        {
            var middle = (start + end) / 2;
            if (nums[middle] == target)
            {
                return middle;
            }
            else if (nums[middle] >= target)
            {
                return FindPosition2(nums, start, middle, target);
            }
            else
            {
                return FindPosition2(nums, middle, end, target);
            }
        }
    }

    public bool IsValidSudoku(char[][] board)
    {
        Dictionary<char, List<int[]>> dict = new Dictionary<char, List<int[]>>();
        var invalid = false;
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i][j] == '.')
                {
                    continue;
                }

                var key = board[i][j];
                var boxIndex = GetBoxIndex(i, j);
                if (!dict.ContainsKey(key))
                {
                    dict.Add(key, new List<int[]> { new int[] { i, j, boxIndex } });
                }
                else
                {
                    foreach (var item in dict[key])
                    {
                        if (item[0] == i || item[1] == j || item[2] == boxIndex)
                        {
                            invalid = true;
                            break;
                        }
                    }
                    if (invalid)
                    {
                        break;
                    }
                    else
                    {
                        dict[key].Add(new int[] { i, j, boxIndex });
                    }
                }
            }
            if (invalid)
            {
                break;
            }
        }

        return !invalid;
    }

    private int GetBoxIndex(int i, int j)
    {
        return (i / 3) * 3 + j / 3;
    }

    public void SolveSudoku(char[][] board)
    {
        var allCharacters = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        var row = new Dictionary<int, List<char>>();
        var column = new Dictionary<int, List<char>>();
        var box = new Dictionary<int, List<char>>();


        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var boxIndex = GetBoxIndex(i, j);
                if (!row.ContainsKey(i))
                {
                    row.Add(i, new List<char>());
                }
                if (!column.ContainsKey(j))
                {
                    column.Add(j, new List<char>());
                }
                if (!box.ContainsKey(boxIndex))
                {
                    box.Add(boxIndex, new List<char>());
                }
                var value = board[i][j];
                if (value != '.')
                {
                    row[i].Add(value);
                    column[j].Add(value);
                    box[boxIndex].Add(value);
                }
            }
        }

        TraceBox(board, allCharacters, 0, row, column, box);
    }

    private bool TraceBox(char[][] board, List<char> allCharacters, int index,
        Dictionary<int, List<char>> row, Dictionary<int, List<char>> column, Dictionary<int, List<char>> box)
    {
        if (index >= 81)
        {
            return true;
        }
        var i = index / 9;
        var j = index - (i) * 9;
        index++;
        if (board[i][j] != '.')
        {
            return TraceBox(board, allCharacters, index, row, column, box);
        }
        else
        {
            var boxIndex = GetBoxIndex(i, j);
            var expectedValues = allCharacters.Except(row[i]).Except(column[j]).Except(box[boxIndex]).ToList();
            if (expectedValues.Count == 0)
            {
                return false;
            }
            else
            {
                var result = false;
                foreach (var value in expectedValues)
                {
                    board[i][j] = value;
                    row[i].Add(value);
                    column[j].Add(value);
                    box[boxIndex].Add(value);
                    var temp = TraceBox(board, allCharacters, index, row, column, box);
                    if (temp)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        board[i][j] = '.';
                        row[i].RemoveAt(row[i].Count - 1);
                        column[j].RemoveAt(column[j].Count - 1);
                        box[boxIndex].RemoveAt(box[boxIndex].Count - 1);
                    }
                }
                return result;
            }
        }
    }


    public string CountAndSay(int n)
    {
        var s = "1";
        while (n > 1)
        {
            s = InnerCountAndSay(s);
            n--;
        }
        return s;
    }

    private string InnerCountAndSay(string s)
    {
        StringBuilder sb = new StringBuilder();
        var count = 1;
        var character = s[0];
        for (int i = 1; i < s.Length; i++)
        {
            if (s[i] == character)
            {
                count++;
                continue;
            }
            else
            {
                sb.Append(count);
                sb.Append(character);
                character = s[i];
                count = 1;
                continue;
            }
        }

        sb.Append(count);
        sb.Append(character);
        return sb.ToString();
    }

    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        candidates = candidates.OrderBy(i => i).ToArray();
        var result = InnerCombinationSum(candidates, 0, target, new List<int>());
        return result;
    }

    private IList<IList<int>> InnerCombinationSum(int[] candidates, int index, int target, IList<int> temp)
    {
        var result = new List<IList<int>>();
        if (index >= candidates.Length || target <= 0)
        {
            return result;
        }

        for (; index < candidates.Length; index++)
        {
            var newTarget = target - candidates[index];
            if (newTarget < 0)
            {
                break;
            }
            else
            {
                var temp2 = temp.Select(t => t).ToList();
                temp2.Add(candidates[index]);
                if (newTarget == 0)
                {
                    result.Add(temp2);
                    break;
                }
                else
                {
                    var list = InnerCombinationSum(candidates, index, newTarget, temp2);
                    if (list != null && list.Count > 0)
                    {
                        result.AddRange(list);
                    }
                }
            }
        }

        return result;
    }

    public IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        candidates = candidates.OrderBy(i => i).ToArray();
        var result = InnerCombinationSum2(candidates, 0, target, new List<int>());
        return result;
    }

    private IList<IList<int>> InnerCombinationSum2(int[] candidates, int index, int target, IList<int> temp)
    {
        var result = new List<IList<int>>();
        if (index >= candidates.Length || target <= 0)
        {
            return result;
        }

        for (var i = index; i < candidates.Length; i++)
        {
            if (i > index && candidates[i] == candidates[i - 1])
            {
                continue;
            }
            var newTarget = target - candidates[i];
            if (newTarget < 0)
            {
                break;
            }
            else
            {
                var temp2 = temp.Select(t => t).ToList();
                temp2.Add(candidates[i]);
                if (newTarget == 0)
                {
                    result.Add(temp2);
                    break;
                }
                else
                {
                    var list = InnerCombinationSum2(candidates, i + 1, newTarget, temp2);
                    if (list != null && list.Count > 0)
                    {
                        result.AddRange(list);
                    }
                }
            }
        }

        return result;
    }

    public int FirstMissingPositive(int[] nums)
    {
        nums = new int[] { 1, 3, 6, 4, 5 };

        var target = nums[0] - 1;
        foreach (var num in nums)
        {
            if (num <= target)
            {
                target = num - 1;
            }
        }
        return target;
    }

    public int firstMissingPositive(int[] nums)
    {
        var n = nums.Length;
        var containsOne = false;
        for (int i = 0; i < n; i++)
        {
            if (nums[i] == 1)
            {
                containsOne = true;
                break;
            }
        }

        if (!containsOne)
        {
            return 1;
        }

        for (int i = 0; i < n; i++)
        {
            if (nums[i] <= 0 || nums[i] > n)
            {
                nums[i] = 1;
            }
        }

        for (int i = 0; i < n; i++)
        {
            var value = Math.Abs(nums[i]);
            nums[value - 1] = -Math.Abs(nums[value - 1]);
        }

        for (int i = 0; i < n; i++)
        {
            if (nums[i] > 0)
            {
                return i + 1;
            }
        }

        return n + 1;
    }

    public int Trap(int[] height)
    {
        var result = 0;
        Stack<int> stack = new Stack<int>();
        for (int i = 1; i < height.Length; i++)
        {
            if (height[i] < height[i - 1])
            {
                stack.Push(i - 1);
            }
            else if (height[i] == height[i - 1])
            {
                continue;
            }
            if (height[i] > height[i - 1])
            {
                var index = i - 1;
                while (stack.Any())
                {
                    var left = stack.First();
                    result += (i - left - 1) * (Math.Min(height[i], height[left]) - height[index]);
                    if (height[left] <= height[i])
                    {
                        index = left;
                        stack.Pop();
                    }

                    if (height[left] >= height[i])
                    {
                        break;
                    }
                }
            }
        }
        return result;
    }

    public int Trap2(int[] height)
    {
        if (height.Length < 3)
        {
            return 0;
        }

        var length = height.Length;
        var left = new int[length];
        var right = new int[length];
        left[0] = height[0];
        right[length - 1] = height[length - 1];
        for (int i = 1; i < length; i++)
        {
            left[i] = Math.Max(left[i - 1], height[i]);
            right[length - 1 - i] = Math.Max(right[length - i], height[length - 1 - i]);
        }
        var result = 0;
        for (int i = 0; i < length; i++)
        {
            result += Math.Min(left[i], right[i]) - height[i];
        }
        return result;
    }

    public string Multiply(string num1, string num2)
    {
        if (num1 == "0" || num2 == "0")
        {
            return "0";
        }
        var temp = new int[num1.Length + num2.Length];
        for (int i = num1.Length - 1; i >= 0; i--)
        {
            var n = num1[i] - '0';
            for (int j = num2.Length - 1; j >= 0; j--)
            {
                var m = num2[j] - '0';
                var sum = n * m;
                temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 1] += sum % 10;
                temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 2] += sum / 10;
                if (temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 1] >= 10)
                {
                    temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 2] += temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 1] / 10;
                    temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 1] = temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 1] % 10;
                }
                if (temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 2] >= 10)
                {
                    temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 3] += temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 2] / 10;
                    temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 2] = temp[temp.Length - (num1.Length - i - 1) - (num2.Length - j - 1) - 2] % 10;
                }
            }
        }

        StringBuilder sb = new StringBuilder();
        var empty = true;
        foreach (var item in temp)
        {
            if (empty && item == 0)
            {
                continue;
            }
            sb.Append(item.ToString());
            empty = false;
        }

        return sb.ToString();
    }

    //第一行表示s为空的情况
    //第一列表示p为空的情况
    public bool IsMatch(string s, string p)
    {
        var dp = new bool[s.Length + 1][];
        for (var i = 0; i < dp.Length; i++)
        {
            dp[i] = new bool[p.Length + 1];
            if (i == 0)
            {
                dp[i][0] = true;
            }
        }

        for (int i = 1; i < p.Length + 1; i++)
        {
            dp[0][i] = dp[0][i - 1] && (p[i - 1] == '*');
        }

        for (int i = 1; i < dp.Length; i++)
        {
            for (int j = 1; j < dp[i].Length; j++)
            {
                if (p[j - 1] == '?' || p[j - 1] == s[i - 1])
                {
                    dp[i][j] = dp[i - 1][j - 1];
                }
                else if (p[j - 1] == '*')
                {
                    dp[i][j] = dp[i - 1][j] || dp[i][j - 1];
                }
            }
        }

        return dp[s.Length][p.Length];
    }

    private bool IsMatch(string s, string p, int sIndex, int pIndex)
    {
        if (sIndex >= s.Length)
        {
            if (pIndex >= p.Length)
            {
                return true;
            }
            else
            {
                var result = true;
                while (pIndex < p.Length)
                {
                    if (p[pIndex] != '*')
                    {
                        result = false;
                        break;
                    }
                    pIndex++;
                }
                return result;
            }
        }
        else
        {
            if (pIndex >= p.Length)
            {
                return false;
            }
            else
            {
                if (p[pIndex] == '?')
                {
                    return IsMatch(s, p, ++sIndex, ++pIndex);
                }
                else if (p[pIndex] == '*')
                {
                    pIndex++;
                    while (pIndex < p.Length && p[pIndex] == '*')
                    {
                        pIndex++;
                    }

                    var result = false;
                    for (int i = sIndex; i <= s.Length; i++)
                    {
                        result = result || IsMatch(s, p, i, pIndex);
                        if (result)
                        {
                            return result;
                        }
                    }
                    return result;
                }
                else
                {
                    return p[pIndex] == s[sIndex] ? IsMatch(s, p, ++sIndex, ++pIndex) : false;
                }

            }
        }
    }


    public int Jump(int[] nums)
    {
        var count = 0;
        var end = 0;
        var maxPos = 0;
        for (var i = 0; i < nums.Length - 1; i++)
        {
            maxPos = Math.Max(maxPos, nums[i] + i);
            if (i == end)
            {
                end = maxPos;
                count++;
            }
        }

        return count;
    }

    public IList<IList<int>> Permute(int[] nums)
    {
        return Permute(nums.ToList(), new List<int>());
    }

    private IList<IList<int>> Permute(List<int> included, List<int> excluded)
    {
        if (included.Count == 0)
        {
            return null;
        }

        var result = new List<IList<int>>();
        for (int i = 0; i < included.Count; i++)
        {
            var item = included[i];
            included.Remove(item);
            excluded.Add(item);
            var temp = Permute(included, excluded);
            if (temp != null)
            {
                foreach (var tempItem in temp)
                {
                    var list = new List<int>();
                    list.Add(item);
                    list.AddRange(tempItem);
                    result.Add(list);
                }
            }
            else
            {
                result.Add(new List<int> { item });
            }
            included.Insert(i, item);
            excluded.Remove(item);

        }

        return result;
    }

    public IList<IList<int>> PermuteUnique(int[] nums)
    {
        return Permute2(nums.OrderBy(i => i).ToList(), new List<int>());
    }

    private IList<IList<int>> Permute2(List<int> included, List<int> excluded)
    {
        if (included.Count == 0)
        {
            return null;
        }

        var result = new List<IList<int>>();
        for (int i = 0; i < included.Count; i++)
        {
            if (i > 0 && included[i] == included[i - 1])
            {
                continue;
            }

            var item = included[i];
            included.Remove(item);
            excluded.Add(item);
            var temp = Permute2(included, excluded);
            if (temp != null)
            {
                foreach (var tempItem in temp)
                {
                    var list = new List<int>();
                    list.Add(item);
                    list.AddRange(tempItem);
                    result.Add(list);
                }
            }
            else
            {
                result.Add(new List<int> { item });
            }
            included.Insert(i, item);
            excluded.Remove(item);

        }

        return result;
    }

    public void Rotate(int[][] matrix)
    {
        var length = matrix.Length;
        for (int i = 0; i < length / 2; i++)
        {
            for (int j = i; j < length - i - 1; j++)
            {
                var temp = matrix[i][j];
                matrix[i][j] = matrix[length - 1 - j][i];
                matrix[length - 1 - j][i] = matrix[length - 1 - i][length - 1 - j];
                matrix[length - 1 - i][length - 1 - j] = matrix[j][length - 1 - i];
                matrix[j][length - 1 - i] = temp;
            }
        }
    }

    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
        foreach (var item in strs)
        {
            var key = new string(item.ToArray().OrderBy(c => c).ToArray());
            if (dict.ContainsKey(key))
            {
                dict[key].Add(item);
            }
            else
            {
                dict.Add(key, new List<string> { item });
            }
        }

        var result = new List<IList<string>>();
        foreach (var item in dict)
        {
            result.Add(item.Value);
        }
        return result;
    }

    public double MyPow(double x, int n)
    {
        if (n == 0)
        {
            return 1;
        }
        else if (n == 1)
        {
            return x;
        }
        else if (n == int.MinValue)
        {
            var temp = MyPow(x, n / 2);
            return temp * temp;
        }
        var abs = Math.Abs(n);
        var result = x;
        var power = 1;
        while (power <= abs / 2)
        {
            result *= result;
            power = power * 2;
        }

        result *= MyPow(x, abs - power);

        if (n < 0)
        {
            result = 1 / result;
        }
        return result;
    }

    public IList<IList<string>> SolveNQueens(int n)
    {
        return SolveNQueens(n, 0, new Dictionary<int, int>());
    }


    private IList<IList<string>> SolveNQueens(int n, int row, Dictionary<int, int> dict)
    {
        List<IList<string>> result = new List<IList<string>>();
        for (int i = 0; i < n; i++)
        {
            if (dict.ContainsKey(row * n + i) && dict[row * n + i] > 0)
            {
                continue;
            }
            else
            {
                if (row >= n - 1)
                {
                    result.Add(new List<string> { GenerateRow(n, i) });
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        var key = row * n + j;
                        AddKey(dict, key);
                    }
                    for (int j = row + 1; j < n; j++)
                    {
                        var offset = j - row;
                        AddKey(dict, n * j + i);
                        if (i - offset >= 0)
                        {
                            AddKey(dict, j * n + (i - offset));
                        }
                        if (i + offset < n)
                        {
                            AddKey(dict, j * n + (i + offset));
                        }
                    }
                    var list = SolveNQueens(n, row + 1, dict);

                    for (int j = row + 1; j < n; j++)
                    {
                        var offset = j - row;
                        dict[n * j + i]--;
                        if (i - offset >= 0)
                        {
                            dict[j * n + (i - offset)]--;
                        }
                        if (i + offset < n)
                        {
                            dict[j * n + (i + offset)]--;
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        var key = row * n + j;
                        dict[key]--;
                    }

                    foreach (var item in list)
                    {
                        var temp = new List<string> { GenerateRow(n, i) };
                        temp.AddRange(item);
                        result.Add(temp);
                    }
                }
            }
        }

        return result;
    }

    private void AddKey<TKey>(Dictionary<TKey, int> dict, TKey key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]++;
        }
        else
        {
            dict.Add(key, 1);
        }
    }

    private string GenerateRow(int n, int index)
    {
        var sb = new StringBuilder();
        var i = 0;
        while (i < n)
        {
            if (i == index)
            {
                sb.Append('Q');
            }
            else
            {
                sb.Append('.');
            }
            i++;
        }
        return sb.ToString();
    }

    public int TotalNQueens(int n)
    {
        return SolveNQueens2(n, 0, new Dictionary<int, int>());
    }

    private int SolveNQueens2(int n, int row, Dictionary<int, int> dict)
    {
        int result = 0;
        for (int i = 0; i < n; i++)
        {
            if (dict.ContainsKey(row * n + i) && dict[row * n + i] > 0)
            {
                continue;
            }
            else
            {
                if (row >= n - 1)
                {
                    result += 1;
                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        var key = row * n + j;
                        AddKey(dict, key);
                    }
                    for (int j = row + 1; j < n; j++)
                    {
                        var offset = j - row;
                        AddKey(dict, n * j + i);
                        if (i - offset >= 0)
                        {
                            AddKey(dict, j * n + (i - offset));
                        }
                        if (i + offset < n)
                        {
                            AddKey(dict, j * n + (i + offset));
                        }
                    }
                    var count = SolveNQueens2(n, row + 1, dict);

                    for (int j = row + 1; j < n; j++)
                    {
                        var offset = j - row;
                        dict[n * j + i]--;
                        if (i - offset >= 0)
                        {
                            dict[j * n + (i - offset)]--;
                        }
                        if (i + offset < n)
                        {
                            dict[j * n + (i + offset)]--;
                        }
                    }
                    for (int j = 0; j < n; j++)
                    {
                        var key = row * n + j;
                        dict[key]--;
                    }

                    result += count;
                }
            }
        }

        return result;
    }

    public int MaxSubArray(int[] nums)
    {
        var result = nums[0];
        var sum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (sum > 0)
            {
                sum += nums[i];
            }
            else
            {
                sum = nums[i];
            }

            result = Math.Max(sum, result);
        }
        return result;
    }

    public IList<int> SpiralOrder(int[][] matrix)
    {
        var result = new List<int>();
        if (matrix.Length == 0)
        {
            return result;
        }
        var height = matrix.Length;
        var width = matrix[0].Length;
        for (int i = 0; i < (height - 1) / 2 + 1; i++)
        {
            for (int j = i; j < width - i; j++)
            {
                result.Add(matrix[i][j]);
            }

            for (int j = i + 1; j < height - i; j++)
            {
                result.Add(matrix[j][width - i - 1]);
            }
            if (width - 2 * i > 1 && height - 2 * i > 1)
            {
                for (int j = width - i - 2; j >= i; j--)
                {
                    result.Add(matrix[height - i - 1][j]);
                }

                for (int j = height - i - 2; j > i; j--)
                {
                    result.Add(matrix[j][i]);
                }
            }

            if (width - 2 * i <= 2)
            {
                break;
            }
        }
        return result;
    }

    public bool CanJump(int[] nums)
    {
        var start = 0;
        var end = 0;
        var minPos = 1;
        var go = true;
        var result = false;
        while (go)
        {
            go = false;
            var maxPos = 0;
            for (int i = start; i <= end; i++)
            {
                maxPos = Math.Max(maxPos, i + nums[i]);
            }

            if (maxPos >= nums.Length - 1)
            {
                result = true;
                break;
            }
            else if (maxPos >= minPos)
            {
                start = end + 1;
                end = maxPos;
                go = true;
                minPos = maxPos + 1;
            }

        }

        return result;
    }

    public bool CanJump2(int[] nums)
    {
        if (nums.Length <= 1)
        {
            return true;
        }

        int zeroCount = 0;
        int zeroStart = nums.Length - 1;
        for (int i = nums.Length - 1; i >= 0; i--)
        {
            if (nums[i] == 0)
            {
                if (zeroCount == 0)
                {
                    zeroStart = i;
                }
                zeroCount++;
            }
            else
            {
                if (zeroCount > 0 && (nums[i] + i > zeroStart || nums[i] + i >= nums.Length - 1))
                {
                    zeroCount = 0;
                }
            }
        }

        return zeroCount == 0;
    }


    public int[][] Merge(int[][] intervals)
    {
        var list = intervals.ToList();

        for (int i = 0; i < list.Count - 1;)
        {
            var needMerge = false;
            for (int j = i + 1; j < list.Count; j++)
            {
                var item1 = list[i];
                var item2 = list[j];
                if (item1[0] <= item2[1] && item2[0] <= item1[1])
                {
                    list[j] = new int[] { Math.Min(item1[0], item2[0]), Math.Max(item1[1], item2[1]) };
                    list.RemoveAt(i);
                    needMerge = true;
                    break;
                }
            }
            if (!needMerge)
            {
                i++;
            }
        }
        return list.ToArray();
    }

    public int[][] Insert(int[][] intervals, int[] newInterval)
    {
        if (intervals.Length == 0)
        {
            return new int[][] { newInterval };
        }
        var list = intervals.ToList();
        var found = false;
        for (int i = 0; i < list.Count;)
        {
            if (found)
            {
                if (list[i][0] > list[i - 1][1])
                {
                    break;
                }
                else
                {
                    list[i - 1][1] = Math.Max(list[i - 1][1], list[i][1]);
                    list.RemoveAt(i);
                }
            }
            else
            {
                if (newInterval[1] < list[i][0])
                {
                    found = true;
                    list.Insert(i, newInterval);
                    break;
                }
                else if (newInterval[1] == list[i][0])
                {
                    found = true;
                    list[i] = new int[] { newInterval[0], list[i][1] };
                    break;
                }
                else if (newInterval[0] > list[i][1])
                {
                    i++;
                }
                else
                {
                    found = true;
                    list[i] = new int[] { Math.Min(newInterval[0], list[i][0]), Math.Max(newInterval[1], list[i][1]) };
                    i++;
                }
            }
        }

        if (!found)
        {
            list.Add(newInterval);
        }

        return list.ToArray();
    }
}
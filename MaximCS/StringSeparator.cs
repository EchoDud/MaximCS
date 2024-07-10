using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximCS
{
    internal class StringSeparator
    {
        public static (string ProcessedString, Dictionary<char, int> CharCount, string LongestVowelSubstring, string SortedString) Do(string input, ISorter sorter)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input can't be empty");
            }

            var invalidChars = input.Where(c => c < 'a' || c > 'z').Distinct().ToArray();
            if (invalidChars.Length > 0)
            {
                throw new ArgumentException($"Invalid characters in input: {new string(invalidChars)}");
            }

            string processedString;
            try
            {
                if ((input.Length & 1) == 0)
                {
                    int mid = input.Length / 2;
                    string firstHalf = string.Concat(input.Take(mid).Reverse());
                    string secondHalf = string.Concat(input.Skip(mid).Reverse());

                    processedString = firstHalf + secondHalf;
                }
                else
                {
                    string reversedInput = string.Concat(input.Reverse());
                    processedString = reversedInput + input;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong", ex);
            }

            var charCount = processedString.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());

            string longestVowelSubstring = FindLongestVowelSubstring(processedString);

            string sortedString = sorter.Sort(processedString);

            return (processedString, charCount, longestVowelSubstring, sortedString);
        }

        private static string FindLongestVowelSubstring(string input)
        {
            HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u', 'y' };
            int start = -1, end = -1, maxLength = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (vowels.Contains(input[i]))
                {
                    for (int j = i; j < input.Length; j++)
                    {
                        if (vowels.Contains(input[j]) && (j - i + 1) > maxLength)
                        {
                            start = i;
                            end = j;
                            maxLength = j - i + 1;
                        }
                    }
                }
            }

            return start != -1 && end != -1 ? input.Substring(start, end - start + 1) : string.Empty;
        }
    }

}

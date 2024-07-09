using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximCS
{
    internal class StringSeparator
    {
        public static (string ProcessedString, Dictionary<char, int> CharCount) Do(string input)
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

            return (processedString, charCount);
        }
    }

}

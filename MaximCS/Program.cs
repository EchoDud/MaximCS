namespace MaximCS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter a string: ");
                string input = Console.ReadLine();

                Console.WriteLine("Choose sorting method (1 for QuickSort, 2 for TreeSort): ");
                string choice = Console.ReadLine();
                ISorter sorter;

                switch (choice)
                {
                    case "1":
                        sorter = new QuickSorter();
                        break;
                    case "2":
                        sorter = new TreeSorter();
                        break;
                    default:
                        throw new ArgumentException("Invalid choice for sorting method");
                }

                var result = StringSeparator.Do(input, sorter);

                Console.WriteLine($"Processed string: {result.ProcessedString}");
                Console.WriteLine("Character count:");
                foreach (var kvp in result.CharCount)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }

                if (!string.IsNullOrEmpty(result.LongestVowelSubstring))
                {
                    Console.WriteLine($"Longest vowel substring: {result.LongestVowelSubstring}");
                }
                else
                {
                    Console.WriteLine("No valid vowel substring found.");
                }

                Console.WriteLine($"Sorted string: {result.SortedString}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

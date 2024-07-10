namespace MaximCS
{
    internal class Program
    {
        static async Task Main(string[] args)
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

                var apiUrl = "https://www.randomnumberapi.com/api/v1.0/random";
                var apiClient = new RandomNumberApiClient(apiUrl);

                var result = await StringSeparator.Do(input, sorter, apiClient);

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

                if (!string.IsNullOrEmpty(result.TrimmedString))
                {
                    Console.WriteLine($"Trimmed string: {result.TrimmedString}");
                }
                else
                {
                    Console.WriteLine("Failed to get trimmed string from API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

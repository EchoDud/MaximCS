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
                var result = StringSeparator.Do(input);

                Console.WriteLine($"Processed string: {result.ProcessedString}");
                Console.WriteLine("Character count:");
                foreach (var kvp in result.CharCount)
                {
                    Console.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

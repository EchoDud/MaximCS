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
                string result = StringSeparator.Do(input);
                Console.WriteLine("Processed string: " + result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

namespace BookCatalog.Services
{
    // Helper class for console input management
    public static class ConsoleHelper
    {
        public static string RequestString(string label, bool required = false)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "exit")
                    throw new OperationCanceledException();

                if (string.IsNullOrWhiteSpace(input) && required)
                {
                    Console.WriteLine($"The field '{label}' is required. Try again or type 'exit' to cancel.");
                    continue;
                }

                return input;
            }
        }

        public static bool RequestBoolean(string label, bool defaultvalue)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine().Trim();
                var result = defaultvalue;
                if (input.ToLower() == "exit")
                    throw new OperationCanceledException();
                switch(input.ToLower()){
                    case var y when input.Contains("y"):
                    case var t when input.Contains("true"):
                        result = true;
                        break;
                    case var n when input.Contains("n"):
                    case var f when input.Contains("false"):
                        result = false;
                        break;
                    default:
                        break;

                }
                return result;
            }
        }

        public static int RequestInteger(string label, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "exit")
                    throw new OperationCanceledException();

                if (int.TryParse(input, out int result) && result >= minValue && result <= maxValue)
                {
                    return result;
                }

                Console.WriteLine($"Enter a valid number between {minValue} and {maxValue}. Try again or type 'exit' to cancel.");
            }
        }

        public static decimal RequestDecimal(string label, decimal minValue = decimal.MinValue, decimal maxValue = decimal.MaxValue)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine().Trim();

                if (input.ToLower() == "exit")
                    throw new OperationCanceledException();

                if (decimal.TryParse(input, out decimal result) && result >= minValue && result <= maxValue)
                {
                    return result;
                }

                Console.WriteLine($"Enter a valid decimal value. Try again or type 'exit' to cancel.");
            }
        }
    }

}

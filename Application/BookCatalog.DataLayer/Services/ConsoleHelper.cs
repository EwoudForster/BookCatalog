namespace BookCatalog.DataLayer.Services
{
    // Helper class for console input management
    public static class ConsoleHelper
    {
        // request a string from the user with validation
        public static string RequestString(string label, bool required = false)
        {
            while (true)
            {
                Console.Write($"{label}: ");
                string input = Console.ReadLine().Trim();

                // if the user types 'exit', cancel the operation
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

        // request a boolean from the user with validation with default value
        public static bool RequestBoolean(string label, bool defaultvalue)
        {
            while (true)
            {
                // request the input
                Console.Write($"{label}: ");
                string input = Console.ReadLine().Trim();
                var result = defaultvalue;

                // if the user types 'exit', cancel the operation
                if (input.ToLower() == "exit")
                    throw new OperationCanceledException();

                // check if the input is a valid boolean value
                switch (input.ToLower()){

                    // check if the input is true and set the result to true
                    case var y when input.Contains("y"):
                    case var t when input.Contains("true"):
                        result = true;
                        break;
                    // check if the input is false and set the result to false
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


        // request an integer from the user with validation
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


        // request a decimal from the user with validation
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

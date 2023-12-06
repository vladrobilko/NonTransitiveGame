using System;
using NonTransitiveGame.Enums;

namespace NonTransitiveGame
{
    public static class MoveValidationDisplayer
    {
        public static void DisplayMessage(this MovesValidationResult result, string[] moves)
        {
            if (result == MovesValidationResult.Success)
                DisplayMoves(moves);
            else
                DisplayErrorMessage(result);
        }

        private static void DisplayMoves(string[] moves)
        {
            Console.WriteLine("Available moves:");

            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }

            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        private static void DisplayErrorMessage(MovesValidationResult result)
        {
            string error = "";
            string condition = "";

            if (result == MovesValidationResult.IncorrectNumberOfParameters)
            {
                error = "Incorrect number of parameters";
                condition = "The number of arguments must not be less than 3.";
            }
            else if (result == MovesValidationResult.NotOddNumberOfArguments)
            {
                error = "Number of arguments is not odd";
                condition = "The number of arguments must be odd.";
            }
            else if (result == MovesValidationResult.NonUniqueElements)
            {
                error = "Elements are not unique.";
                condition = "All elements must be unique.";
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {error}");
            Console.WriteLine("\nPlease ensure the following condition is met:");
            Console.WriteLine($"   {condition}\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Example:");
            Console.WriteLine("   dotnet run -- value1 value2 value3\n");
            Console.ResetColor();
            Console.WriteLine("Thank you.");
        }
    }
}

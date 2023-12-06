using System.Collections.Generic;
using NonTransitiveGame.Enums;

namespace NonTransitiveGame
{
    public static class MovesValidator
    {
        public static MovesValidationResult GetValidationResult(this string[] moves)
        {
            if (moves.Length < 3)
            {
                return MovesValidationResult.IncorrectNumberOfParameters;
            }

            else if (moves.Length % 2 == 0)
            {
                return MovesValidationResult.NotOddNumberOfArguments;
            }

            else if (HasDuplicates(moves))
            {
                return MovesValidationResult.NonUniqueElements;
            }

            return MovesValidationResult.Success;
        }

        private static bool HasDuplicates(string[] array)
        {
            var uniqueElements = new HashSet<string>(array);

            return array.Length != uniqueElements.Count;
        }
    }
}
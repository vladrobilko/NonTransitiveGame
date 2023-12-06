using System.Linq;
using ConsoleTables;
using NonTransitiveGame.Enums;

namespace NonTransitiveGame
{
    public static class TableMovesGenerator
    {
        public static void Generate(this string[] moves)
        {
            string[,] result = new string[moves.Length + 1, moves.Length + 1];

            FillArray(result, moves);

            int rowCount = result.GetLength(0);
            int colCount = result.GetLength(1);

            var table = new ConsoleTable();

            table.AddColumn(result.Cast<string>().Take(colCount));

            for (int i = 1; i < rowCount; i++)
            {
                var rowData = new object[colCount];

                for (int j = 0; j < colCount; j++)
                {
                    rowData[j] = result[i, j];
                }

                table.AddRow(rowData);
            }

            table.Write(Format.Minimal);
        }

        private static void FillArray(string[,] array, string[] moves)
        {
            array[0, 0] = "v PC/User";

            FillFirstRowAndColumn(array, moves);

            FillAllResults(array, moves);
        }

        private static void FillAllResults(string[,] array, string[] moves)
        {
            for (int i = 1; i < array.GetLength(0); i++)
            {
                for (int j = 1; j < array.GetLength(0); j++)
                {
                    int playerMove = i;
                    int botMove = j - 1;

                    GameResult result = GameResultEvaluator.GetWinner(moves, playerMove, botMove);

                    switch (result)
                    {
                        case GameResult.PlayerWins:
                            array[i, j] = "Lose";
                            break;
                        case GameResult.BotWins:
                            array[i, j] = "Win";
                            break;
                        case GameResult.Draw:
                            array[i, j] = "Draw";
                            break;
                    }
                }
            }
        }

        private static void FillFirstRowAndColumn(string[,] array, string[] moves)
        {
            for (int j = 1; j < array.GetLength(0); j++)
            {
                array[0, j] = moves[j - 1];
                array[j, 0] = moves[j - 1];
            }
        }
    }
}
using System.Collections.Generic;
using NonTransitiveGame.Enums;

namespace NonTransitiveGame
{
    public static class GameResultEvaluator
    {
        public static GameResult GetWinner(string[] moves, int playerMove, int botMove)
        {
            if (playerMove - 1 == botMove) return GameResult.Draw;

            var botMovesToLosePlayer = GetBotMovesToLosePlayer(moves, playerMove);

            return botMovesToLosePlayer.Contains(moves[botMove]) ? GameResult.BotWins : GameResult.PlayerWins;
        }

        private static List<string> GetBotMovesToLosePlayer(string[] moves, int playerMove)
        {
            var botMovesToLosePlayer = new List<string>();

            int indexOfLoseParameter = playerMove - 1;

            for (int i = 0; i < (moves.Length - 1) / 2; i++)
            {
                indexOfLoseParameter = (indexOfLoseParameter + 1) % moves.Length;
                botMovesToLosePlayer.Add(moves[indexOfLoseParameter]);
            }

            return botMovesToLosePlayer;
        }
    }
}
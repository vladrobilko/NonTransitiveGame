using System;
using NonTransitiveGame.Enums;
using NonTransitiveGame.Security;

namespace NonTransitiveGame
{
    public class NonTransitiveGame
    {
        private readonly string[] _moves;

        private readonly Random _random;

        public NonTransitiveGame(string[] moves)
        {
            _moves = moves;
            _random = new Random();
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine("--------------------------------------------------------------------------");

                var validationResult = _moves.GetValidationResult();

                if (validationResult != MovesValidationResult.Success)
                {
                    validationResult.DisplayMessage(_moves);
                    break;
                }

                var key = KeyGenerator.GenerateRandomKey(32);

                var botMove = BotMove();

                var hmac = HmacGenerator.CalculateHmac(key, _moves[botMove]);

                Console.WriteLine($"HMAC: {hmac}");

                validationResult.DisplayMessage(_moves);

                var playerMove = PlayerMove();

                if (playerMove == 0)
                {
                    Console.WriteLine("Exiting the game. Goodbye!");
                    break;
                }

                PlayRound(playerMove, botMove, key);

                Console.WriteLine("--------------------------------------------------------------------------");

                Console.WriteLine("Press ESCAPE to exit or any button to continue");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape) break;
            }
        }

        private void PlayRound(int playerMove, int botMove, byte[] key)
        {
            var result = GameResultEvaluator.GetWinner(_moves, playerMove, botMove);

            ShowMoves(playerMove, botMove);

            ShowResult(result);

            Console.WriteLine($"HMAC key: {BitConverter.ToString(key).Replace("-", "")}");
        }

        private void ShowMoves(int playerMove, int botMove)
        {
            Console.WriteLine($"Your move: {_moves[playerMove - 1]}");

            Console.WriteLine($"Computer move: {_moves[botMove]}");
        }

        private static void ShowResult(GameResult result)
        {
            if (result == GameResult.PlayerWins)
                Console.WriteLine("You win");
            else if (result == GameResult.BotWins)
                Console.WriteLine("Bot win");
            else
                Console.WriteLine("Draw");
        }

        private int PlayerMove()
        {
            Console.Write("Enter your move: ");

            var playerInput = Console.ReadLine();

            if (playerInput == "?")
            {
                _moves.Generate();

                Console.Write("Enter your move: ");

                playerInput = Console.ReadLine();
            }

            var success = int.TryParse(playerInput ?? "0", out int playerMove);

            if (playerMove == 0) return 0;

            return success ? playerMove : 0;
        }

        private int BotMove()
        {
            int botMove = _random.Next(0, _moves.Length);

            return botMove;
        }
    }
}
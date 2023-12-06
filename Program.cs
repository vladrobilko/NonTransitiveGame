namespace NonTransitiveGame
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var game = new NonTransitiveGame(args);
            game.Start();
        }
    }
}

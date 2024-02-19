using _420J13AS_2024_RPSLS.AI;

namespace _420J13AS_2024_RPSLS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = Game.Create<RandomAI>();

            game.Challenge<RockOnlyAI>();
            //game.Challenge<GenericOneAI>();
            //game.Challenge<CircularAI>();
            //game.Challenge<FavoriteOneAI>();

            game.End();
        }
    }
}

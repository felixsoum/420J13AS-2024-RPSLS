namespace _420J13AS_2024_RPSLS.AI.Dummy
{
    internal class GenericOneAI : DummyAI
    {
        readonly Move favoriteMove;

        public GenericOneAI()
        {
            favoriteMove = RandomMove();
            Nickname = $"{favoriteMove} Enthusiast";
        }

        public override Move Play()
        {
            return favoriteMove;
        }
    }
}

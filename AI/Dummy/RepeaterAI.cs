namespace _420J13AS_2024_RPSLS.AI.Dummy
{
    class RepeaterAI : DummyAI
    {
        int repeatCount;
        int repeatIndex;
        List<Move> movePattern = new List<Move>();

        public RepeaterAI()
        {
            Nickname = "Aramis";
            repeatCount = Game.SeededRandom.Next(20, 40);
            for (int i = 0; i < repeatCount; i++)
            {
                movePattern.Add(RandomMove());
            }
        }

        public override Move Play()
        {
            repeatIndex %= movePattern.Count;
            return movePattern[repeatIndex++];
        }
    }
}

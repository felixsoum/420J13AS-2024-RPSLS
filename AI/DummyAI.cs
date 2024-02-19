namespace _420J13AS_2024_RPSLS.AI
{
    public abstract class DummyAI : BaseAI
    {
        public int DummyLevel { get; set; } = 1;

        protected Move[] shuffledMoves = (Move[])(Enum.GetValues(typeof(Move)));

        public override string GetAuthor()
        {
            return base.GetAuthor() + $" Lv.{DummyLevel}";
        }

        protected void Shuffle()
        {
            for (int i = 4; i > 0; i--)
            {
                int j = Game.SeededRandom.Next(0, i + 1);
                Move temp = shuffledMoves[i];
                shuffledMoves[i] = shuffledMoves[j];
                shuffledMoves[j] = temp;
            }
        }
    }
}

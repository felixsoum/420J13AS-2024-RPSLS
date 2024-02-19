namespace _420J13AS_2024_RPSLS.AI
{
    internal class CircularAI : DummyAI
    {
        readonly Move[] circularMoves = new Move[]
       {
            Move.Scissors,
            Move.Paper,
            Move.Rock,
            Move.Lizard,
            Move.Spock
       };

        int moveIndex;

        public CircularAI()
        {
            Nickname = "Trickster";
            moveIndex = Game.SeededRandom.Next(5);
        }

        public override Move Play()
        {
            moveIndex %= 5;
            return circularMoves[moveIndex++];
        }
    }
}

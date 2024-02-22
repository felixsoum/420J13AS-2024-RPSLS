namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class DULA : StudentAI
    {
        private readonly Move[] circularMoves = new Move[]
{
Move.Lizard,
Move.Rock,
Move.Scissors,
Move.Paper,
Move.Spock
};

        private int moveIndex;
        private int consecutiveSameMovesCount;

        public DULA()
        {
            Nickname = "FerrisBueller";
            moveIndex = Game.SeededRandom.Next(5);
            consecutiveSameMovesCount = 0;
        }

        public override Move Play()
        {
            // Déterminer le mouvement avec une probabilité différente
            Move nextMove;
            double probability = Game.SeededRandom.NextDouble();

            if (probability < 0.2)
            {
                // 20% de chance de jouer papier
                nextMove = Move.Paper;
            }
            else if (probability < 0.4)
            {
                // 20% de chance de jouer roche
                nextMove = Move.Rock;
            }
            else
            {
                // 60% de chance de jouer un mouvement circulaire
                if (consecutiveSameMovesCount < 5)
                {
                    consecutiveSameMovesCount++;
                }
                else
                {
                    moveIndex = (moveIndex + 1) % 5;
                    consecutiveSameMovesCount = 0;
                }

                nextMove = circularMoves[moveIndex];
            }

            return nextMove;
        }
    }

}

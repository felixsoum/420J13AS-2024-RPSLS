namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class DIAS : BaseAI
    {
        private Random rand;

        public IEnumerable<object> OpponentMoves { get; private set; }

        public DIAS()
        {
            Nickname = "Bob";
            rand = Game.SeededRandom;
        }

        public override void Observe(Move opponentMove)
        {
            base.Observe(opponentMove);
        }

        public override Move Play()
        {
            // Play a move that can defeat the most probable move of the opponent
            Move opponentPrediction = GetOpponentPrediction();
            Move myMove = GetCounterMove(opponentPrediction);
            return myMove;
        }

        private Move GetOpponentPrediction()
        {
            // Predict the opponent's move based on their previous moves
            int[] moveCounts = new int[5]; // Rock, Paper, Scissors, Spock, Lizard

            foreach (var move in OpponentMoves)
            {
                moveCounts[(int)move]++;
            }

            // Find the most probable move of the opponent
            int maxCount = 0;
            int predictedMoveIndex = 0;
            for (int i = 0; i < moveCounts.Length; i++)
            {
                if (moveCounts[i] > maxCount)
                {
                    maxCount = moveCounts[i];
                    predictedMoveIndex = i;
                }
            }

            return (Move)predictedMoveIndex;
        }

        private Move GetCounterMove(Move opponentMove)
        {
            // Determine the move that can defeat the opponent's move
            switch (opponentMove)
            {
                case Move.Rock:
                    return Move.Paper; // Paper covers Rock
                case Move.Paper:
                    return Move.Scissors; // Scissors cuts Paper
                case Move.Scissors:
                    return Move.Rock; // Rock crushes Scissors
                case Move.Spock:
                    return Move.Lizard; // Lizard poisons Spock
                case Move.Lizard:
                    return Move.Scissors; // Scissors decapitates Lizard
                default:
                    throw new Exception("Invalid opponent move.");
            }
        }
    }
}

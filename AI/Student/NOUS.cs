namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class NOUS : StudentAI
    {
        private Dictionary<Move, int> Frequences = new Dictionary<Move, int>();

        public NOUS()
        {
            Nickname = "Shiro";

            foreach (Move move in System.Enum.GetValues(typeof(Move)))
            {
                Frequences[move] = 0;
            }
        }

        public override void Observe(Move opponentMove)
        {
            base.Observe(opponentMove);

            if (Frequences.ContainsKey(opponentMove))
            {
                Frequences[opponentMove]++;
            }
        }

        public override Move Play()
        {
            Move mostFrequent = Frequent();

            Move move = Contre(mostFrequent);
            return move;
        }

        private Move Contre(Move move)
        {
            switch (move)
            {
                case Move.Rock:
                case Move.Scissors:
                    return Move.Spock;
                case Move.Paper:
                case Move.Lizard:
                    return Move.Scissors;
                case Move.Spock:
                    return Move.Paper;
                default:
                    return Move.Rock;
            }
        }

        private Move Frequent()
        {
            Move mostFrequent = Move.Rock;
            int max = 0;
            foreach (var move in Frequences)
            {
                if (move.Value > max)
                {
                    mostFrequent = move.Key;
                    max = move.Value;
                }
            }
            return mostFrequent;
        }
    }
}

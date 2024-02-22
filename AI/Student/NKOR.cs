namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class NKOR : StudentAI
    {
        private Dictionary<Move, int> movecounts;
        private bool setcircular = false;
        private int turncount = 0;
        readonly Move[] circularMoves = new Move[]
        {
Move.Scissors,
Move.Paper,
Move.Rock,
Move.Lizard,
Move.Spock
        };
        public NKOR()
        {
            Nickname = "Gameryan";
            movecounts = new Dictionary<Move, int>();
            foreach (Move move in Enum.GetValues(typeof(Move)))
            {
                movecounts[move] = 0;
            }

        }

        public override Move Play()
        {


            // etait les condition si ennemi est circular
            // if (setcircular == false) Iscircular();
            //if (setcircular)
            //{ return circularMoves[( - 2) % circularMoves.Length]; }

            // game 0
            if (movecounts.Values.Sum() == 0)
            {
                return RandomMove();
            }


            Move opponentmove = Populaire();


            Move countermove = Contre(opponentmove);


            return countermove;
        }
        private void Indexvalue(Move opponentmove)
        {
            // etait senser retourner l index du move
        }
        public override void Observe(Move opponentMove)
        {

            movecounts[opponentMove]++;

        }

        private Move Populaire()
        {
            Move plusjouer = Move.Rock;
            int maxcount = 0;
            foreach (var a in movecounts)
            {
                if (a.Value > maxcount)
                {
                    plusjouer = a.Key;
                    maxcount = a.Value;
                }
            }
            return plusjouer;
        }

        private Move Contre(Move opponentMove)
        {

            switch (opponentMove)
            {
                case Move.Rock:
                    return Move.Paper;
                case Move.Paper:
                    return Move.Scissors;
                case Move.Scissors:
                    return Move.Rock;
                case Move.Spock:
                    return Move.Lizard;
                case Move.Lizard:
                    return Move.Scissors;
                default:

                    return RandomMove();
            }
        }
        private void Iscircular()
        {
            // les condition etait pour tester
            for (int i = 0; i <= circularMoves.Length - 1; i++)
            {
                if (movecounts.ContainsKey(circularMoves[i]))
                {
                    setcircular = true;
                }
                else
                {
                    setcircular = false;
                    break;
                }
            }
        }
    }
}

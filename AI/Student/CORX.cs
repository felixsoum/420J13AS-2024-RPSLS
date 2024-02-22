namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class CORX : StudentAI
    {
        int ConsecutiveMoves = 0;
        Move OponentLastMove;
        List<Move> Moves = new List<Move>();
        int MovesInOrder = 0;
        int ExchangeCounter = 0;
        bool DontRepeat = true;
        Move CommonMove;
        bool SpamMove = false;

        Move[] AllMoves =
        {
        Move.Scissors,
        Move.Paper,
        Move.Rock,
        Move.Lizard,
        Move.Spock
    };

        int[] PreviousMoveList = new int[5];

        public CORX()
        {
            Nickname = "Our AI";
        }


        public override void Observe(Move opponentMove)
        {
            ExchangeCounter++;
            if (opponentMove == OponentLastMove)
            {
                ConsecutiveMoves++;
            }
            else
            {
                ConsecutiveMoves = 0;
            }

            if (ConsecutiveMoves < 3)
            {
                foreach (int I in PreviousMoveList)
                {
                    if (DontRepeat)
                    {
                        if (I > (ExchangeCounter / 5) + 1)
                        {
                            DontRepeat = false;
                        }
                    }
                }
                if (DontRepeat == true)
                {
                    MovesInOrder++;
                }
                DontRepeat = true;
            }

            for (int i = 0; i < PreviousMoveList.Length; i++)
            {
                if (PreviousMoveList[i] >= (ExchangeCounter / 5) + 2)
                {
                    switch (i)
                    {
                        case 0:
                            CommonMove = Move.Rock;
                            break;
                        case 1:
                            CommonMove = Move.Paper;
                            break;
                        case 2:
                            CommonMove = Move.Scissors;
                            break;
                        case 3:
                            CommonMove = Move.Spock;
                            break;
                        case 4:
                            CommonMove = Move.Lizard;
                            break;
                    }
                    SpamMove = true;
                    break;
                }

            }

            switch (opponentMove)
            {
                case Move.Rock:
                    PreviousMoveList[0]++;
                    break;
                case Move.Paper:
                    PreviousMoveList[1]++;
                    break;
                case Move.Scissors:
                    PreviousMoveList[2]++;
                    break;
                case Move.Spock:
                    PreviousMoveList[3]++;
                    break;
                case Move.Lizard:
                    PreviousMoveList[4]++;
                    break;
            }

            OponentLastMove = opponentMove;
        }

        public override Move Play()
        {

            if (ConsecutiveMoves == 0 && MovesInOrder == 0)
            {
                return LosingMove((Move)OponentLastMove)[(Game.SeededRandom.Next(0, 2))];
            }

            if (ConsecutiveMoves >= 3)
            {
                if (OponentLastMove != Move.Lizard)
                {
                    return (Move)OponentLastMove + 1;
                }
                else
                {
                    return Move.Rock;
                }
            }
            else if (MovesInOrder >= 9)
            {
                return (Move)((int)(OponentLastMove + 2) % 5);
            }

            if (SpamMove)
            {
                return WinNeutralMove(CommonMove)[(Game.SeededRandom.Next(0, 2))];
            }
            return RandomMove();
        }

        public Move[] LosingMove(Move opponentMove)
        {
            foreach (Move move in AllMoves)
            {
                switch ((move - opponentMove + 5) % 5)
                {
                    default:
                    case 0:
                        break;
                    case 1:
                    case 3:
                        break;
                    case 2:
                    case 4:
                        Moves.Add(move);
                        break;
                }
            }
            return Moves.ToArray();
        }

        public Move[] WinningMove(Move opponentMove)
        {
            foreach (Move move in AllMoves)
            {
                switch ((move - opponentMove + 5) % 5)
                {
                    default:
                    case 0:
                        break;
                    case 1:
                    case 3:
                        Moves.Add(move);
                        break;
                    case 2:
                    case 4:
                        break;
                }
            }
            return Moves.ToArray();
        }

        public Move[] WinNeutralMove(Move opponentMove)
        {
            foreach (Move move in AllMoves)
            {
                switch ((move - opponentMove + 5) % 5)
                {
                    default:
                    case 0:
                        Moves.Add(move);
                        break;
                    case 1:
                    case 3:
                        Moves.Add(move);
                        break;
                    case 2:
                    case 4:
                        break;
                }
            }
            return Moves.ToArray();
        }
    }
}

namespace _420J13AS_2024_RPSLS.AI.Dummy
{
    class FavoriteTwoAI : DummyAI
    {
        List<Move> moves = new List<Move>();
        int index;

        public FavoriteTwoAI()
        {
            Nickname = "Porthos";
            Move firstFavoriteMove = RandomMove();
            Move secondFavoriteMove;

            switch (firstFavoriteMove)
            {
                case Move.Rock:
                    secondFavoriteMove = Move.Spock;
                    break;
                case Move.Paper:
                    secondFavoriteMove = Move.Lizard;
                    break;
                case Move.Scissors:
                    secondFavoriteMove = Move.Rock;
                    break;
                case Move.Spock:
                    secondFavoriteMove = Move.Paper;
                    break;
                case Move.Lizard:
                default:
                    secondFavoriteMove = Move.Scissors;
                    break;
            }

            for (int i = 0; i < 5; i++)
            {
                if (i == (int)firstFavoriteMove || i == (int)secondFavoriteMove)
                {
                    continue;
                }
                int count = Game.SeededRandom.Next(6) + 10;
                for (int j = 0; j < count; j++)
                {
                    moves.Add((Move)i);
                }
            }

            int half = (100 + moves.Count) / 2;

            while (moves.Count < half)
            {
                moves.Add(secondFavoriteMove);
            }

            while (moves.Count < 100)
            {
                moves.Add(firstFavoriteMove);
            }

            for (int i = 99; i > 0; i--)
            {
                int j = Game.SeededRandom.Next(i + 1);
                Move temp = moves[j];
                moves[j] = moves[i];
                moves[i] = temp;
            }
        }

        public override Move Play()
        {
            return moves[index++];
        }
    }
}

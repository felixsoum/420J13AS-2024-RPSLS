namespace _420J13AS_2024_RPSLS.AI.Dummy
{
    internal class FavoriteOneAI : DummyAI
    {
        List<Move> moves = new List<Move>();
        int index;

        public FavoriteOneAI()
        {
            Nickname = "Athos";
            Move favoriteMove = RandomMove();
            for (int i = 0; i < 5; i++)
            {
                if (i == (int)favoriteMove)
                {
                    continue;
                }
                int count = Game.SeededRandom.Next(6) + 10;
                for (int j = 0; j < count; j++)
                {
                    moves.Add((Move)i);
                }
            }
            while (moves.Count < 100)
            {
                moves.Add(favoriteMove);
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

namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class GAGX : StudentAI
    {
        int index = 0;
        Move[] allmoves = new Move[20];
        Move moveopponent;

        public GAGX()
        {
            Nickname = "Xavier";
        }

        public override Move Play()
        {
            return Move.Paper;
        }

        //J'ai commencé le Observe mais honnêtement je sais pas trop quoi en faire faque c'est ça
        public override void Observe(Move opponentMove)
        {
            moveopponent = opponentMove;
            allmoves[index] = moveopponent;
            index++;

            if (index == 20)
            {
                index = 0;
            }
        }
    }
}

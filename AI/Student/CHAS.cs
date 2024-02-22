namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class CHAS : StudentAI
    {

        private List<string> movesHistory = new List<string>();

        public CHAS()
        {
            Nickname = "John JR";
        }

        public override Move Play()
        {
            return ChooseAMove();
        }

        public override void Observe(Move opponentMove)
        {
            AddAdversaireMove(opponentMove);
        }

        private void AddAdversaireMove(Move opponentMove)
        {
            movesHistory.Add(opponentMove.ToString());
            if (movesHistory.Count > 20)
            {
                movesHistory.RemoveAt(0);
            }
        }

        private string PreditMove()
        {
            if (movesHistory.Count == 0)
            {
                return RandomMove().ToString();
            }
            else
            {
                //Move communMove = Move.Rock;
                List<string> tempList = new List<string>();
                tempList = movesHistory;
                tempList.GroupBy(x => x);
                tempList.OrderByDescending(g => g.Count());

                return tempList.FirstOrDefault();
            }
        }

        private Move ChooseAMove()
        {

            string movePredit = PreditMove();
            Move moveChoosed = Move.Spock;

            switch (movePredit)
            {
                case "Rock":
                    moveChoosed = Move.Paper;
                    break;
                case "Paper":
                    moveChoosed = Move.Scissors;
                    break;
                case "Scissors":
                    moveChoosed = Move.Spock;
                    break;
                case "Spock":
                    moveChoosed = Move.Lizard;
                    break;
                case "Lizard":
                    moveChoosed = Move.Rock;
                    break;
            }

            return moveChoosed;
        }
    }
}

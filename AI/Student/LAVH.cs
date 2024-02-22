namespace _420J13AS_2024_RPSLS.AI.Student
{
 internal class LAVH : StudentAI
{
    int posTab = 0;
    Move[] mouvesPrecedant = new Move[5];
    Move moveAdversePrecedent;
    public LAVH()
    {
        Nickname = "Epic Gamer";
    }
 
    public override Move Play()
    {
        int rockCount = 0;
        int paperCount = 0;
        int ScissorsCount = 0;
        int SpockCount = 0;
        int LizardCount = 0;
        int movePrecPos = 0;
        for (int i = 0; i < mouvesPrecedant.Length; i++)
        {
            if (mouvesPrecedant[i] == moveAdversePrecedent)
                movePrecPos = i;
            if (mouvesPrecedant[i] == Move.Rock)
            {
                rockCount++;
            }
            else if (mouvesPrecedant[i] == Move.Paper)
            {
                paperCount++;
            }
            else if (mouvesPrecedant[i] == Move.Scissors)
            {
                ScissorsCount++;
            }
            else if (mouvesPrecedant[i] == Move.Spock)
            {
                SpockCount++;
            }
            else if (mouvesPrecedant[i] == Move.Lizard)
            {
                LizardCount++;
            }
        }
        if (rockCount == 1 && paperCount == 1 && ScissorsCount == 1 && SpockCount == 1 && LizardCount == 1)
        {
            if (movePrecPos == 4)
            {
                movePrecPos = 0;
            }
            else movePrecPos++;
            switch (mouvesPrecedant[movePrecPos])
            {
                case Move.Rock:
                    {
                        int choix = Game.SeededRandom.Next(0, 2);
                        if (choix == 0)
                        {
                            return Move.Paper;
                        }
                        else
                        {
                            return Move.Spock;
                        }
                    }
                case Move.Paper:
                    {
                        int choix = Game.SeededRandom.Next(0, 2);
                        if (choix == 0)
                        {
                            return Move.Scissors;
                        }
                        else
                        {
                            return Move.Lizard;
                        }
                    }
                case Move.Scissors:
                    {
                        int choix = Game.SeededRandom.Next(0, 2);
                        if (choix == 0)
                        {
                            return Move.Rock;
                        }
                        else
                        {
                            return Move.Spock;
                        }
                    }
                case Move.Lizard:
                    {
                        int choix = Game.SeededRandom.Next(0, 2);
                        if (choix == 0)
                        {
                            return Move.Scissors;
                        }
                        else
                        {
                            return Move.Rock;
                        }
                    }
                case Move.Spock:
                    {
                        int choix = Game.SeededRandom.Next(0, 2);
                        if (choix == 0)
                        {
                            return Move.Paper;
                        }
                        else
                        {
                            return Move.Lizard;
                        }
                    }
                default:
                    {
                        return RandomMove();
                    }
 
            }
        }
        else if(ScissorsCount >= 3)
        {
            int choix = Game.SeededRandom.Next(0, 2);
            if (choix == 0)
            {
                return Move.Rock;
            }
            else
            {
                return Move.Spock;
            }
        }
        else if (rockCount >= 3)
        {
            int choix = Game.SeededRandom.Next(0, 2);
            if (choix == 0)
            {
                return Move.Paper;
            }
            else
            {
                return Move.Spock;
            }
        }
        else if (paperCount >= 3)
        {
            int choix = Game.SeededRandom.Next(0, 2);
            if (choix == 0)
            {
                return Move.Scissors;
            }
            else
            {
                return Move.Lizard;
            }
        }
        else if (SpockCount >= 3)
        {
            int choix = Game.SeededRandom.Next(0, 2);
            if (choix == 0)
            {
                return Move.Paper;
            }
            else
            {
                return Move.Lizard;
            }
        }
        else if (LizardCount >= 3)
        {
            int choix = Game.SeededRandom.Next(0, 2);
            if (choix == 0)
            {
                return Move.Scissors;
            }
            else
            {
                return Move.Rock;
            }
        }
        else
        {
            return RandomMove();
        }
        ////ce que j'avais fait initiallement
        //switch (moveAdversePrecedent)
        //{
        //    case Move.Rock:
        //        {
        //            int choix = new Random().Next(0, 2);
        //            if (choix == 0)
        //            {
        //                return Move.Paper;
        //            }
        //            else
        //            {
        //                return Move.Spock;
        //            }
        //        }
        //    case Move.Paper:
        //        {
        //            int choix = new Random().Next(0, 2);
        //            if (choix == 0)
        //            {
        //                return Move.Scissors;
        //            }
        //            else
        //            {
        //                return Move.Lizard;
        //            }
        //        }
        //    case Move.Scissors:
        //        {
        //            int choix = new Random().Next(0, 2);
        //            if (choix == 0)
        //            {
        //                return Move.Rock;
        //            }
        //            else
        //            {
        //                return Move.Spock;
        //            }
        //        }
        //    case Move.Lizard:
        //        {
        //            int choix = new Random().Next(0, 2);
        //            if (choix == 0)
        //            {
        //                return Move.Scissors;
        //            }
        //            else
        //            {
        //                return Move.Rock;
        //            }
        //        }
        //    case Move.Spock:
        //        {
        //            int choix = new Random().Next(0, 2);
        //            if (choix == 0)
        //            {
        //                return Move.Paper;
        //            }
        //            else
        //            {
        //                return Move.Lizard;
        //            }
        //        }
        //    default:
        //        {
        //            return RandomMove();
        //        }
 
        //}
    }
 
    public override void Observe(Move opponentMove)
    {
        moveAdversePrecedent = opponentMove;
 
        mouvesPrecedant[posTab] = opponentMove;
        posTab++;
        if (posTab == 5)
        {
            posTab = 0;
        }
 
    }
}
}

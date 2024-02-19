namespace _420J13AS_2024_RPSLS.AI.Dummy
{
    internal class RandomAI : BaseAI
    {
        public RandomAI()
        {
            Nickname = "Gambler";
        }

        public override Move Play()
        {
            return RandomMove();
        }
    }
}

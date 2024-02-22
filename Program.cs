using _420J13AS_2024_RPSLS.AI.Dummy;

namespace _420J13AS_2024_RPSLS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = Game.Create<RandomAI>();

            //game.Challenge<RockOnlyAI>();
            //game.Challenge<GenericOneAI>();
            //game.Challenge<CircularAI>();
            //game.Challenge<FavoriteOneAI>();

            //game.Challenge<FavoriteTwoAI>();
            //game.Challenge<RepeaterAI>();

            //game.PlayTournament();

            game.End();
        }
    }
}

public class MyHashMap
{
    int taillemax = 1000;
    List<KeyValPair>[] laliste;

    public MyHashMap()
    {
        laliste = new List<KeyValPair>[taillemax];
    }

    public void Put(int key, int value)
    {
        int lacle = key % taillemax;

        if (laliste[lacle] is null)
        {
            laliste[lacle] = new List<KeyValPair>() { new KeyValPair(key, value) };
            return;
        }

        var valeur = laliste[lacle].FirstOrDefault(x => x.Cle == key);

        if (valeur is null)
        {
            laliste[lacle].Add(new KeyValPair(key, value));
            return;
        }

        valeur.Val = value;
    }

    public int Get(int key)
    {
        int lacle = key % taillemax;

        if (laliste[lacle] is null)
            return -1;

        var valeur = laliste[lacle].FirstOrDefault(x => x.Cle == key);

        if (valeur is null)
            return -1;

        return valeur.Val;
    }

    public void Remove(int key)
    {
        int lacle = key % taillemax;

        if (laliste[lacle] is null)
            return;

        laliste[lacle].RemoveAll(x => x.Cle == key);
    }

    public class KeyValPair
    {
        public int Cle { get; set; }
        public int Val { get; set; }

        public KeyValPair(int key, int val)
        {
            Cle = key;
            Val = val;
        }
    }
}
namespace _420J13AS_2024_RPSLS.AI.Student
{
    internal class GAUM : StudentAI
    {
        // constante du nombre de moves possible
        readonly int NUM_OF_MOVES = ((Move[])Enum.GetValues(typeof(Move))).Length;

        // array qui tient compte du nb de fois que chaque move a été utilisé par l'oposant
        private int[] favoris_opposant = new int[((Move[])Enum.GetValues(typeof(Move))).Length];
        // liste des moves faits par l'oposant
        private List<int> previous_moves_opposant = new List<int>();
        // variable de retour mis comme global pour être accessible
        private int pattern_result;
        // nb du tour (premier = 0)
        private int turn_count = 0;

        public GAUM()
        {
            // THEEEEEE UNDERTAKEEEEEEEERRRRRRRR
            Nickname = "Malcolm G.P.T.";
        }

        public override Move Play()
        {
            // valeure numérique du move le plus utilisé par l'opposant
            int index_of_max_favori = Array.IndexOf(favoris_opposant, favoris_opposant.Max());

            // si le move le plus utilisé est utilisé plus que un tiers des fois
            if (favoris_opposant.Max() > favoris_opposant.Sum() / 3)
            {
                // toujours battre ce move populaire
                return (Move)((index_of_max_favori + 1) % NUM_OF_MOVES);
            }

            // cela ne vaut pas trouver un pattern avant que deux rotations ont pus être fait
            if (turn_count >= NUM_OF_MOVES * 2)
            {
                // essaye de voir si il y a un pattern dans les moves
                if (FindPattern())
                {
                    // bat le prochain move qu'on expecte
                    return (Move)((pattern_result + 1) % NUM_OF_MOVES);
                }
            }

            // augmente le nb du tour
            turn_count++;
            // si aucun favori et aucun pattern trouvé, hasard.
            return RandomMove();
        }

        // update previous_moves_opposant et favoris_opposant
        public override void Observe(Move opponentMove)
        {
            previous_moves_opposant.Add((int)opponentMove);
            favoris_opposant[(int)opponentMove]++;
        }

        // retourne true si pattern trouvé. malheureusement, ne peux seulement chercher pour des patterns de longueure 5 ou moins...
        private bool FindPattern()
        {
            // deux arrays à comparer plus tard
            int[] section1 = new int[NUM_OF_MOVES];
            int[] section2 = new int[NUM_OF_MOVES];

            // trouver patterns de 1 à 5
            for (int i = 1; i <= 5; i++)
            {
                // copier les derniers 2i elements de la liste dans deux arrays de taille égale
                previous_moves_opposant.CopyTo(previous_moves_opposant.Count - i, section1, 0, i);
                previous_moves_opposant.CopyTo(previous_moves_opposant.Count - i * 2, section2, 0, i);

                // pas besoin de vider les arrays après chaque itération, chaque itération overrite tout les vielles données

                // vérifie égalité par valeure. si égaux, pattern possible trouvé
                if (Enumerable.SequenceEqual(section1, section2))
                    break;
            }

            // on modifie la variable globale pour dire c'est quoi qu'on pense être le prochain, qui devrait être le premier
            // de n'importe quelle des deux listes.
            pattern_result = section2[0];
            // return si égalité trouvée
            return Enumerable.SequenceEqual(section1, section2);
        }
    }
}

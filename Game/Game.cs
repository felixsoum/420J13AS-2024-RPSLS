using _420J13AS_2024_RPSLS.AI.Dummy;
using _420J13AS_2024_RPSLS.AI.Student;
using System.Diagnostics;
using System.Reflection;

namespace _420J13AS_2024_RPSLS
{
    class Contestant : IComparable<Contestant>
    {
        public Type AI { get; set; }
        public string Nickname { get; set; }
        public string Author { get; set; }
        public int WinCount { get; set; }

        public Contestant(Type ai, string nickname, string author)
        {
            AI = ai;
            Nickname = nickname;
            Author = author;
        }

        public override string ToString()
        {
            return $"{Nickname} ({Author}) has {WinCount} wins.";
        }

        public int CompareTo(Contestant? other)
        {
            return -WinCount.CompareTo(other.WinCount);
        }
    }

    public static class MoveComparator
    {
        public static int CompareWith(this Move move1, Move move2)
        {
            if (IsInvalid(move1) && IsInvalid(move2))
            {
                return 0;
            }
            else if (IsInvalid(move1))
            {
                return 1;
            }
            else if (IsInvalid(move2))
            {
                return -1;
            }
            switch ((move1 - move2 + 5) % 5)
            {
                default:
                case 0:
                    return 0;
                case 1:
                case 3:
                    return 1;
                case 2:
                case 4:
                    return -1;
            }
        }

        public static bool IsInvalid(Move move)
        {
            return move < 0 || (int)move > 4;
        }
    }

    public enum Move
    {
        Rock,
        Paper,
        Scissors,
        Spock,
        Lizard
    }

    public sealed class Game
    {
        public bool IsLogging { get; set; } = true;
        string LogPath;
        const int RoundMax = 100;
        string log;
        List<string> fullLog = new List<string>();
        static bool isInstantiated;
        public static int Mutex { get; private set; }
        public static Random SeededRandom { get; private set; }
        const int Seed = 22 + 6 + 7;
        const int BattleCount = 20;
        readonly Type studentAI;

        public static Game Create<T>() where T : BaseAI
        {
            if (!isInstantiated)
            {
                isInstantiated = true;
                return new Game(typeof(T));
            }
            return null;
        }

        private Game(Type studentAI)
        {
            this.studentAI = studentAI;
            LogPath = Path.Combine("..", "..", "..", "BattleLog.txt");
            SetSeed(Seed);
        }

        public int Battle(BaseAI ai1, BaseAI ai2)
        {
            Stopwatch stopWatch = new Stopwatch();
            int ai1LongestTurn = 0;
            int ai2LongestTurn = 0;
            Move move1;
            Move move2;
            Move? prevMove1 = null;
            Move? prevMove2 = null;
            int ai1WinCount = 0;
            int ai2WinCount = 0;
            Log($"{ai1} ({ai1.GetAuthor()}) VS {ai2} ({ai2.GetAuthor()}):\n", true);

            for (int i = 0; i < RoundMax; i++)
            {
                stopWatch.Start();
                if (prevMove2.HasValue)
                {
                    ai1.Observe(prevMove2.Value);
                }
                move1 = ClampMove(ai1.Play());
                stopWatch.Stop();
                ai1LongestTurn = (int)stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();

                stopWatch.Start();
                if (prevMove1.HasValue)
                {
                    ai2.Observe(prevMove1.Value);
                }
                move2 = ClampMove(ai2.Play());
                stopWatch.Stop();
                ai2LongestTurn = (int)stopWatch.ElapsedMilliseconds;
                stopWatch.Reset();

                prevMove1 = move1;
                prevMove2 = move2;

                Log($"Round {i + 1}: ");

                switch (move1.CompareWith(move2))
                {
                    case 0:
                        Log($"{ai1} and {ai2} both played {move1}.\n");
                        break;
                    case 1:
                        ai1WinCount++;
                        Log($"{ai1}'s {move1} beats {ai2}'s {move2}.\n");
                        break;
                    case -1:
                        ai2WinCount++;
                        Log($"{ai2}'s {move2} beats {ai1}'s {move1}.\n");
                        break;
                }
            }
            string outcomeMessage = $"{ai1} won {ai1WinCount} rounds and {ai2} won {ai2WinCount} rounds.\n";

            if (ai1WinCount > ai2WinCount)
            {
                outcomeMessage += $"{ai1} ({ai1.GetAuthor()}) defeats {ai2} ({ai2.GetAuthor()})!\n\n";
            }
            else if (ai1WinCount < ai2WinCount)
            {
                outcomeMessage += $"{ai2} ({ai2.GetAuthor()}) defeats {ai1} ({ai1.GetAuthor()})!\n\n";
            }
            else
            {
                outcomeMessage += $"{ai1} ({ai1.GetAuthor()}) ties with {ai2} ({ai2.GetAuthor()})!\n\n";
            }
            if (ai1LongestTurn > 10)
            {
                outcomeMessage += $"{ai1} is disqualified because longest turn took {ai1LongestTurn}ms!\n\n";
            }

            if (ai2LongestTurn > 10)
            {
                outcomeMessage += $"{ai2} is disqualified because longest turn took {ai2LongestTurn}ms!\n\n";
            }

            Log(outcomeMessage, true);

            if (IsLogging)
            {
                fullLog.Add(log);
            }
            return ai1WinCount.CompareTo(ai2WinCount);
        }

        public int Battle<T1, T2>() where T1 : BaseAI where T2 : BaseAI
        {
            return Battle(CreateAI<T1>(), CreateAI<T2>());
        }

        public int Battle<T>() where T : BaseAI
        {
            return Battle(CreateStudentAI(), CreateAI<T>());
        }

        public void SetBattleCount(int n)
        {
            log = "";
            Log($"Battle {n} ================================================================\n", true);
        }

        public void StartLog()
        {
            if (IsLogging)
            {
                fullLog.Clear();
            }
        }

        public void Log(string message, bool andPrint = false)
        {
            if (IsLogging)
            {
                log += message;
                if (andPrint)
                {
                    Console.Write(message);
                }
            }
        }

        public void End()
        {
            if (IsLogging)
            {
                Console.WriteLine($"Please see log at {Path.GetFullPath(LogPath)} for more details.");
                File.WriteAllLines(LogPath, fullLog);
            }
            isInstantiated = false;
        }

        public void ResetMutex()
        {
            Mutex = 0;
        }

        public static void IncrementMutex()
        {
            Mutex++;
        }

        public void SetSeed(int seed)
        {
            SeededRandom = new Random(seed);
        }

        BaseAI CreateStudentAI()
        {
            return CreateAI(studentAI);
        }

        BaseAI CreateAI(Type type)
        {
            ResetMutex();
            return (BaseAI)Activator.CreateInstance(type);
        }

        BaseAI CreateAI<T>() where T : BaseAI
        {
            ResetMutex();
            return Activator.CreateInstance<T>();
        }

        public void Challenge<T>() where T : BaseAI
        {
            int seed = Seed;
            BaseAI player1 = null;
            BaseAI player2 = null;
            StartLog();
            bool isSuccess = true;
            for (int i = 0; i < BattleCount; i++)
            {
                SetSeed(seed++);
                player1 = CreateStudentAI();
                player2 = CreateAI<T>();
                SetBattleCount(i + 1);
                if (Battle(player1, player2) != 1)
                {
                    isSuccess = false;
                }
            }
            Log($"!!! Challenge against {player2.GetAuthor()} " + (isSuccess ? "passed" : "failed") + " !!! \n\n", true);
        }

        public void PlayTournament()
        {
            IsLogging = false;
            var contestants = new List<Contestant>();
            foreach (var type in Assembly.GetEntryAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(StudentAI)))
                {
                    try
                    {
                        StudentAI studentAI = (StudentAI)CreateAI(type);

                        studentAI.Play();

                        contestants.Add(new Contestant(type, studentAI.Nickname, studentAI.GetAuthor()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error AI: {type.Name}, {e.Message}");
                    }
                }
            }

            int studentCount = contestants.Count;
            Console.WriteLine($"\n{studentCount} students entered in tournament.\n");
            Console.WriteLine($"Some student AIs are slow, so please be patient and wait.\n");

            int dummyIndex = 0;
            var dummyTypes = new Type[] { typeof(RockOnlyAI), typeof(FavoriteOneAI), typeof(CircularAI), typeof(GenericOneAI) };
            Type currentDummyType = null;
            for (int i = 0; i < studentCount * 9; i++)
            {
                dummyIndex %= dummyTypes.Length;
                currentDummyType = dummyTypes[dummyIndex++];
                contestants.Add(new Contestant(currentDummyType, currentDummyType.Name, ""));
            }

            Contestant c1 = null;
            Contestant c2 = null;
            for (int i = 0; i < studentCount; i++)
            {
                c1 = contestants[i];
                for (int j = i + 1; j < contestants.Count; j++)
                {
                    c2 = contestants[j];
                    int result = Battle(CreateAI(c1.AI), CreateAI(c2.AI));
                    if (result == 1)
                    {
                        c1.WinCount++;
                    }
                    else if (result == -1)
                    {
                        c2.WinCount++;
                    }
                }
            }

            for (int i = contestants.Count - 1; i >= studentCount * 9; i--)
            {
                contestants.RemoveAt(i);
            }
            contestants.Sort();

            Console.ReadLine();

            Console.WriteLine("Leaderboard________________________________");

            for (int i = 0; i < studentCount; i++)
            {
                Console.WriteLine(contestants[i]);
                Console.ReadLine();
            }
            Console.WriteLine("FIN");
            Console.ReadLine();
        }

        Move ClampMove(Move move)
        {
            if (move < Move.Rock || move > Move.Lizard)
            {
                return Move.Rock;
            }
            return move;
        }
    }
}
// LAB2

public class Programm
{
    private class GameAccount
    {
        protected string? UserName;
        protected int CurentRating;
        private int GameCount;
        private int GameWinCount = Reset;
        public string AccountType;

        protected const int LoseRaitingBoost = -5;
        protected const int WinRaitingBoost = 10;
        protected int WinStreak = Reset;
        protected int LoseStreak = Reset;
        protected const int Reset = 0;

        public virtual string GetuserName() { return UserName; }
        
        public GameAccount(string? userName)
        {
            this.UserName = userName;
            this.CurentRating = 100;
            this.GameCount = 0;
        }

        public virtual void WinGame(GameType type) {}

        public void WinGame()
        {
            GameCount++;
            WinStreak++;
            GameWinCount++;
            LoseStreak = Reset;
        }
        public virtual void LoseGame(GameType type){}

        public void LoseGame()
        {
            LoseStreak++;
            WinStreak = Reset;
            GameCount++;
        }
        public void show_stats()
        {
            Console.WriteLine($"Інформація про гравця: {UserName}\n\tТип Акаунту: {AccountType}\n\tКількість ігор: {GameCount}\n\tКількість перемог: {GameWinCount}\n\tПоточний рейтинг: {CurentRating}\n");
            Console.WriteLine("");
        }
    }

    private class BasicGameAccount : GameAccount
    {
        private const int LoseFactor = 2;
        private const int WinFactor = 1;
        public BasicGameAccount(string? userName) : base(userName)
        {
            AccountType = "Basic";
        }
        
        public override void WinGame(GameType Type)
        {
            base.WinGame();
            if (WinStreak > 2)
                CurentRating += WinRaitingBoost * (WinFactor + WinStreak)*Type.gameMOD;
            else
                CurentRating += WinRaitingBoost*WinFactor*Type.gameMOD;
        }

        public override void LoseGame(GameType Type)
        {
            base.LoseGame();
            if(CurentRating>5/LoseFactor)
                CurentRating += LoseRaitingBoost / LoseFactor * Type.gameMOD;
        }
    }
    
    private class VipGameAccount : GameAccount
    {
        private const int LoseFactor = 4;
        private const int WinFactor = 2;

        public override string GetuserName() { return UserName + " {VIP}"; }
        public VipGameAccount(string? userName) : base(userName)
        {
            AccountType = "VIP";
        }
        
        public override void WinGame(GameType Type)
        {
            base.WinGame();
            CurentRating += WinRaitingBoost * (WinFactor + WinStreak) * Type.gameMOD;
        }

        public override void LoseGame(GameType Type)
        {
            base.LoseGame();
            if (CurentRating > 5 / LoseFactor)
            {
                if(LoseStreak < 3)
                    CurentRating += LoseRaitingBoost/LoseFactor* Type.gameMOD;
                else
                    CurentRating += (LoseRaitingBoost/LoseFactor-LoseStreak)*Type.gameMOD;
            }
        }
    }
    
    
    private class Game
    {
        string Basic = "Basic";
        string VIP = "VIP";

        private string GameTraining = "Training";
        private string GameBasic = "Basic";
        private string GameTriple = "Triple";
        private enum Names
        {
            Vanya = 0,
            Svitlana = 1,
            Anya = 2,
            Dasha = 3,
            Artur = 4,
            Vlad = 5,
            Denis = 6,
            Kostya = 7,
            Moonlight = 8
        }

        private List<GameAccount> accounts = new();
        private List<GameType> games = new();

        Random rand = new Random(DateTime.UtcNow.Millisecond);

        public Game() { Play(); }
        public void generateplayer(string Type)
        {
            if (Type == VIP)
            {
                Console.Write("Створення нового VIP акаунту... ");
                addplayer(VIP);
                return;
            }
            
            Console.Write("Оберіть кількість Базових акаунтів, яка буде створена: ");
            try
            {
                int numberOfAccounts = Convert.ToInt32(Console.ReadLine());
                if (numberOfAccounts <= 0)
                {
                    throw new Exception("Неможливо створити від'ємну кількість користувачів, або ту, що дорівнює 0");
                }
                for (int i = 0; i < numberOfAccounts; i++)
                { addplayer(Basic); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                generateplayer(Basic);
            }
        }
        
        private void addplayer(string Type)
        {
            if(Type == Basic) 
                accounts.Add(new BasicGameAccount(Enum.GetName(typeof(Names), rand.Next(0,8))));
            else
                accounts.Add(new VipGameAccount(Enum.GetName(typeof(Names), rand.Next(0,8))));
        }

        private void Play()
        {
            Console.WriteLine("Початок Гри....Створення нових користувачів...\n");
            generateplayer(Basic);
            generateplayer(VIP);
            Console.WriteLine("\nПочаток гри:");
            do
            {
                Randomgame();
                Console.Write("Бажаєте почати ще одну гру?(y/n): ");
            } while (Console.ReadLine() == "y");
            
            Console.WriteLine($"Виберіть номер гравця для відображення статистики(1 - {accounts.Count}): ");
            accounts[Convert.ToInt32(Console.ReadLine())-1].show_stats();
            show_history();
        }

        private void Randomgame()
        {
            int playerone = rand.Next(0,accounts.Count-1);
            int playertwo = rand.Next(0,accounts.Count-1);
            int winner = -1;

            while (playerone == playertwo) {playertwo = rand.Next(0,accounts.Count-1);}
            
            
            Console.Write("Оберіть ТИП гри:\n\t<0> Тренувальна\n\t<1> Звичайна\n\t<2> Гра на 3 гравці\nВибір:");
            int choosenGameType;
            choosenGameType = Convert.ToInt32(Console.ReadLine());

            switch (choosenGameType)
            {
                case 1: //Basic
                    winner = rand.Next(1, 2);
                    games.Add(new BasicGameType(accounts[playerone], accounts[playertwo], winner));
                    break;
                    
                case 2: //Triple
                    int playerthree = accounts.Count-1;
                    winner = rand.Next(1, 3);
                    while (playerthree == playerone || playerthree == playertwo) {playerthree = rand.Next(0,accounts.Count-1);}
                    games.Add(new TripleGameType(accounts[playerone], accounts[playertwo], accounts[playerthree], winner));
                    break;
                    
                case 0: //Training
                    winner = rand.Next(1, 2);
                    games.Add(new TrainingGameType(accounts[playerone], accounts[playertwo], winner));
                    break;
                    
                default:
                    Console.WriteLine("Помилка...");
                    Randomgame();
                    break;
            }
            games[^1].StartGame();
            games[^1] = new GameType(games[^1]);
        }


        public void show_history()
        {
            Console.WriteLine($"Історія Ігор (Всього {games.Count} ігор відбулося):");
            foreach (var game in games)
            {
                game.Viewer();
            }
        }
        

    }


    private class HistoryNote
    {
        private readonly GameAccount playerone;
        private readonly GameAccount playertwo;
        private readonly GameAccount playerthree;
        
        private readonly string? gameType;
        private readonly int winner;
        private readonly int gameID;

        private readonly bool triplehistory;
        
        public HistoryNote(GameAccount playerone, GameAccount playertwo, GameAccount playerthree, string? gameType, int winner, int gameID) : this(playerone, playertwo, gameType, winner, gameID)
        {
            this.playerthree = playerthree;
            triplehistory = true;
        }

        public HistoryNote(GameAccount playerone, GameAccount playertwo, string? gameType, int winner, int gameID)
        {
            this.playerone = playerone;
            this.playertwo = playertwo;
            this.winner = winner;
            this.gameType = gameType;
            this.gameID = gameID;
        }

        public void ShowNote()
        {
            if (triplehistory == true)
            {
                Console.Write($"Інфо по грі #{gameID}\n\tТип гри:{gameType}\n\tПерший гравець: {playerone.GetuserName()}\n\tДругий Гравець: {playertwo.GetuserName()}\n\tТретій гравець: {playerthree.GetuserName()}\n\tПереможець: ");
                switch (winner)
                {
                    case 1:
                        Console.Write(playerone.GetuserName());
                        break;
                
                    case 2:
                        Console.Write(playertwo.GetuserName());
                        break;

                    case 3:
                        Console.Write(playerthree.GetuserName());
                        break;
                }
                Console.WriteLine("\n-------------------------");
            }
            else
            {
                Console.WriteLine($"Інфо по грі #{gameID}\n\tТип гри:{gameType}\n\tПерший гравець: {playerone.GetuserName()}\n\tДругий Гравець: {playertwo.GetuserName()}\n\t Переможець: {(winner == 1 ? playerone.GetuserName() : playertwo.GetuserName())}\n-------------------------");
            }
        }
    }
    private class GameType
    {
        protected GameAccount playerone;
        protected GameAccount playertwo;
        protected GameAccount playerthree;
        protected HistoryNote history;
        
        protected string? gameType;
        protected int winner;
        protected int gameID;
        public int gameMOD;
        protected static int GameCount=0;

        protected string Basic = "Basic";
        protected string Training = "Training";
        protected string Triple = "Triple";
        

        protected GameType(GameAccount playerone, GameAccount playertwo, int winner)
        {
            this.playerone = playerone;
            this.playertwo = playertwo;
            this.winner = winner; 
        }

        public GameType(GameType New)
        {
            this.playerone = New.playerone;
            this.playertwo = New.playertwo;
            this.playerthree = New.playerthree;
            this.winner = New.winner;
            this.gameType = New.gameType;
            this.gameID = New.gameID;
            this.history = New.history;
        }

        public void StartGame()
        {
            gameID = GameCount++; 
            GameSimuliation(); 
            Note();
            ShowNote();
        }

        protected virtual void GameSimuliation(){}

        protected virtual void Note(){}

        private void ShowNote() { history.ShowNote(); }
        public void Viewer(){ ShowNote(); }

    }

    private class BasicGameType : GameType
    {
        public BasicGameType(GameAccount playerone, GameAccount playertwo, int winner) : base(playerone,playertwo,winner)
        {
            gameType = Basic;
            gameMOD = 1;
        }

        protected override void GameSimuliation()
        {
            switch (winner)
            {
                case 1:
                    playerone.WinGame(this);
                    playertwo.LoseGame(this);
                    break;
                
                case 2:
                    playertwo.WinGame(this);
                    playerone.LoseGame(this);
                    break;

            }
        }

        protected override void Note()
        {
            history = new HistoryNote(playerone, playertwo, gameType, winner, gameID);
        }
    }
    
    private class TrainingGameType : BasicGameType
    {
        public TrainingGameType(GameAccount playerone, GameAccount playertwo, int winner) : base(playerone,playertwo,winner)
        {
            gameType = Training;
            gameMOD = 0;
        }
    }
    
    private class TripleGameType : GameType
    {
        public TripleGameType(GameAccount playerone, GameAccount playertwo, GameAccount playerthree, int winner) : base(playerone,playertwo,winner)
        {
            gameType = Triple;
            this.playerthree = playerthree;
            gameMOD = 1;
        }
        
        protected override void GameSimuliation()
        {
            switch (winner)
            {
                case 1:
                    playerone.WinGame(this);
                    playertwo.LoseGame(this);
                    playerthree.LoseGame(this);
                    break;
                
                case 2:
                    playertwo.WinGame(this);
                    playerone.LoseGame(this);
                    playerthree.LoseGame(this);
                    break;
                
                case 3:
                    playerthree.WinGame(this);
                    playerone.LoseGame(this);
                    playertwo.LoseGame(this);
                    break;
            }
        }

        protected override void Note()
        {
            history = new HistoryNote(playerone, playertwo, playerthree, gameType, winner, gameID);
        }
    }
    
    
    public static void Main(String[] args)
    {
        Game game = new Game();
    }
}
public class Programm
{
    private class GameAccount
    {
        public string? userName { get; }
        private int curentRating;
        private int gameCount;

        private const int LoseRaitingBoost = -5;
        private const int WinRaitingBoost = 10;

        public GameAccount(string? userName)
        {
            this.userName = userName;
            this.curentRating = 100;
            this.gameCount = 0;
        }

        public void WinGame()
        {
            gameCount++;
            curentRating += WinRaitingBoost;
        }

        public void LoseGame()
        {
            gameCount++;
	    if(curentRating>5)
            curentRating += LoseRaitingBoost;
        }
        
        public void show_stats()
        {
            Console.WriteLine($"Інформація про гравця: {userName}\n\tКількість ігор: {gameCount}\n\tПоточний рейтинг: {curentRating}");
        }
    }

    private class History
    {
        private static int NumberOfGame = 0;
        private int gameNumber;
        private string? playeronename;
        private string? playertwoname;
        private int winner; // 1/2

        public History(string? playeronename, string? playertwoname, int winner)
        {
            this.playeronename = playeronename;
            this.playertwoname = playertwoname;
            this.winner = winner;
            this.gameNumber = NumberOfGame++;
        }
        
        public void show_history()
        {
            Console.Write($"Номер гри: {gameNumber}\n\tПерший гравець: {playeronename}\n\tДругий гравець: {playertwoname}\n\tПереможець: ");
            Console.WriteLine((winner == 1? playeronename : playertwoname) + "\n---------------------------------");
        }
    }

    private class Game
    {
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
        private List<History> history = new();
        
        Random rand = new Random();

        public void generateplayer()
        {
            Console.Write("Оберіть кількість акаунтів, яка буде створена: ");
            int NumberOfAccounts;
            try
            {
                NumberOfAccounts = Convert.ToInt32(Console.ReadLine());
                if (NumberOfAccounts <= 1)
                {
                    throw new Exception("Неможливо створити від'ємну кількість користувачів, або ту, що дорівнює менше 1");
                }
                for (int i = 0; i < NumberOfAccounts; i++)
                { addplayer(); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                generateplayer();
            }
        }
        private void addplayer()
        {
            accounts.Add(new GameAccount(Enum.GetName(typeof(Names), rand.Next(0,8))));
        }

        public void Play()
        {
            Console.WriteLine("Початок Гри....Створення нових користувачів...\n");
            generateplayer();
            Console.WriteLine("Початок гри:");
            do
            {
                randomgame();
                Console.Write("Бажаєте почати ще одну гру?(y/n): ");
            } while (Console.ReadLine() == "y");
            
            Console.WriteLine($"Виберіть номер гравця для відображення статистики(1 - {accounts.Count}): ");
            accounts[Convert.ToInt32(Console.ReadLine())-1].show_stats();
            show_history();
        }

        private void randomgame()
        {
        int playerone = rand.Next(0,accounts.Count-1);
        int playertwo = rand.Next(0,accounts.Count-1);
        int winner = rand.Next(1, 2);
        while (playerone == playertwo)
            {playertwo = rand.Next(0,accounts.Count-1);}

        if (winner == 1)
        {
            accounts[playerone].WinGame();
            accounts[playertwo].LoseGame();
        }
        else
        {
            accounts[playerone].LoseGame();
            accounts[playertwo].WinGame();
        }
        history.Add(new History(accounts[playerone].userName, accounts[playertwo].userName, winner));
        Console.WriteLine("Результати гри:\n\tПереможець: " + (winner == 1? accounts[playerone].userName : accounts[playertwo].userName) + " + 10 до рейтингу.\n\tПереможений: " + (winner == 2? accounts[playerone].userName : accounts[playertwo].userName) + " - 5 до рейтингу");
        }
        
        public void show_history()
        {
            Console.WriteLine("Історія усіх ігор:");
            foreach (var note in history)
                note.show_history();
        }
        

    }

    public static void Main(String[] args)
    {
        Game game = new Game();
        game.Play();
    }
}
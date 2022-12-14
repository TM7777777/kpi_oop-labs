using TicTacToe.Classes;
using System;

namespace TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            StartGame();
        }
        static void StartGame()
        {
            Console.WriteLine("Гравець 1: Введіть імя: ");
            string playerOne = Console.ReadLine();
            Console.WriteLine("Гравець 2: Введіть імя:");
            string playerTwo = Console.ReadLine();

            InstantiateGame(playerOne, playerTwo);

        }
        static void InstantiateGame(string p1, string p2)
        {
            Player playerOne = new Player()
            {
                Name = p1,
                Marker = "X",
                IsTurn = true

            };

            Player playerTwo = new Player()
            {
                Name = p2,
                Marker = "O",
                IsTurn = false

            };
            Console.Clear();

            Game newGame = new Game(playerOne, playerTwo);

            Player winner = newGame.Play();


            if (!(winner is null))
            {
                Console.WriteLine($"{winner.Name} Виграв!");
            }
            else
            {
                Console.WriteLine("Це нічія!");
            }
        }
    }


}


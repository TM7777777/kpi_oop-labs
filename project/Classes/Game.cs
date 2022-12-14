using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Classes
{
    public class Game
    {
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public Player Winner { get; set; }
        public Board Board { get; set; }

        public Game(Player p1, Player p2)
        {
            PlayerOne = p1;
            PlayerTwo = p2;
            Board = new Board();
        }
        public Player Play()
        {


            int counter = 1;
            while (!CheckForWinner(Board))
            {
                Board.DisplayBoard();

                if (PlayerOne.IsTurn)
                {
                    PlayerOne.TakeTurn(Board);
                    counter++;
                    SwitchPlayer();

                }
                else
                {
                    PlayerTwo.TakeTurn(Board);
                    counter++;
                    SwitchPlayer();
                }

                CheckForWinner(Board);

                if (counter >= 9)
                {
                    return Winner;
                }

                Console.Clear();


            }

            Board.DisplayBoard();

            return Winner;

        }

        public bool CheckForWinner(Board board)
        {

            int[][] winners = new int[][]
            {
                new[] {1,2,3},
                new[] {4,5,6},
                new[] {7,8,9},

                new[] {1,4,7},
                new[] {2,5,8},
                new[] {3,6,9},

                new[] {1,5,9},
                new[] {3,5,7}
            };


            for (int i = 0; i < winners.Length; i++)
            {
                Position p1 = Player.PositionForNumber(winners[i][0]);
                Position p2 = Player.PositionForNumber(winners[i][1]);
                Position p3 = Player.PositionForNumber(winners[i][2]);

                string a = Board.GameBoard[p1.Row, p1.Column];
                string b = Board.GameBoard[p2.Row, p2.Column];
                string c = Board.GameBoard[p3.Row, p3.Column];

                if (a + b + c == "XXX")
                {
                    Winner = PlayerOne;
                    return true;
                }
                else if (a + b + c == "OOO")
                {
                    Winner = PlayerTwo;
                    return true;
                }
            }
            return false;
        }

        public Player NextPlayer()
        {
            return (PlayerOne.IsTurn) ? PlayerOne : PlayerTwo;
        }

        public void SwitchPlayer()
        {
            if (PlayerOne.IsTurn)
            {
                PlayerOne.IsTurn = false;
                PlayerTwo.IsTurn = true;
            }
            else
            {
                PlayerOne.IsTurn = true;
                PlayerTwo.IsTurn = false;
            }
        }


    }
}

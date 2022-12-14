using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public string Marker { get; set; }

        public bool IsTurn { get; set; }

        public Position GetPosition(Board board)
        {
            Position desiredCoordinate = null;
            while (desiredCoordinate is null)
            {
                Console.WriteLine("Виберіть позицію");
                Int32.TryParse(Console.ReadLine(), out int position);
                desiredCoordinate = PositionForNumber(position);
            }
            return desiredCoordinate;

        }

        public static Position PositionForNumber(int position)
        {
            switch (position)
            {
                case 1: return new Position(0, 0); // Top Left
                case 2: return new Position(0, 1); // Top Middle
                case 3: return new Position(0, 2); // Top Right
                case 4: return new Position(1, 0); // Middle Left
                case 5: return new Position(1, 1); // Middle Middle
                case 6: return new Position(1, 2); // Middle Right
                case 7: return new Position(2, 0); // Bottom Left
                case 8: return new Position(2, 1); // Bottom Middle 
                case 9: return new Position(2, 2); // Bottom Right

                default: return null;
            }
        }

        public void TakeTurn(Board board)
        {
            IsTurn = true;

            Console.WriteLine($"{Name} твоя черга");

            Position position = GetPosition(board);

            if (Int32.TryParse(board.GameBoard[position.Row, position.Column], out int _))
            {
                board.GameBoard[position.Row, position.Column] = Marker;
            }
            else
            {
                Console.WriteLine("Це місце вже зайнято");
            }
        }
    }
}

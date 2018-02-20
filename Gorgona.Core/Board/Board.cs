using System.Collections;
using Gorgona.Exceptions;

namespace Gorgona.Core
{
    public static class Board
    {
        public enum WorldSide { North, East, South, West, NorthEast, NorthWest, SouthEast, SouthWest }
        private static char[] _squares;

        public static char[] Squares
        {
            set { _squares = value; }
        }

        #region Static Constructor

        static Board()
        {
            _squares = new char[]
            {
                'r', 'b', 's', 'g', 'k',
                'o', 'o', 'o', 'o', 'p',
                'o', 'o', 'o', 'o', 'o',
                'P', 'o', 'o', 'o', 'o',
                'K', 'G', 'S', 'B', 'R'
            };
        }

        #endregion

        #region Methods

        public static bool IsRealSquare((int, int) coordinates)
        {
            return coordinates.Item1 >= 1
                && coordinates.Item1 <= 5
                && coordinates.Item2 >= 1
                && coordinates.Item2 <= 5;
        }

        public static bool IsEdgeSquare((int, int) coordinates, WorldSide worldSide)
        {
            if (!IsRealSquare(coordinates))
            {
                throw new CoordinatesOutOfRangeException("Coordinate values are out of range.");
            }

            switch (worldSide)
            {
                case WorldSide.North:
                    return coordinates.Item2 == 1;

                case WorldSide.East:
                    return coordinates.Item1 == 1;

                case WorldSide.South:
                    return coordinates.Item2 == 5;

                case WorldSide.West:
                    return coordinates.Item1 == 5;

                case WorldSide.NorthEast:
                    return coordinates.Item2 == 1 || coordinates.Item1 == 1;

                case WorldSide.NorthWest:
                    return coordinates.Item2 == 1 || coordinates.Item1 == 5;

                case WorldSide.SouthEast:
                    return coordinates.Item2 == 5 || coordinates.Item1 == 1;

                case WorldSide.SouthWest:
                    return coordinates.Item2 == 5 || coordinates.Item1 == 5;

                default:
                    return false; // Dummy
            }
        }

        public static int GetIndex((int, int) coordinates)
        {
            if (!IsRealSquare(coordinates))
            {
                throw new CoordinatesOutOfRangeException("Coordinate values are out of range.");
            }

            return 5 * coordinates.Item2 - coordinates.Item1;
        }

        public static bool IsSquareOccupied((int, int) coordinates)
        {
            int index = GetIndex(coordinates);
            return _squares[index] != 'o'; ;
        }

        public static char GetPiece((int, int) coordinates)
        {
            return _squares[GetIndex(coordinates)];
        }


        public static bool IsSquareAvailable((int, int) coordinates, int side)
        {
            char piece = GetPiece(coordinates);

            if (piece == 'o')
            {
                return true;
            }
            else
            {
                return side == 1 
                    ? char.IsLower(piece) 
                    : char.IsUpper(piece);
            }
        }
       
        #endregion
    }
}

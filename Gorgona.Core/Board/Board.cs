using System.Collections;
using Gorgona.Exceptions;

namespace Gorgona.Core.Board
{
    public static class Board
    {
        private static char[] _squares;
        private static Hashtable _dictionary;

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

            _dictionary = new Hashtable();
            _dictionary.Add(0, (5, 1));
            _dictionary.Add(1, (4, 1));
            _dictionary.Add(2, (3, 1));
            _dictionary.Add(3, (2, 1));
            _dictionary.Add(4, (1, 1));
            _dictionary.Add(5, (5, 2));
            _dictionary.Add(6, (4, 2));
            _dictionary.Add(7, (3, 2));
            _dictionary.Add(8, (2, 2));
            _dictionary.Add(9, (1, 2));
            _dictionary.Add(10, (5, 3));
            _dictionary.Add(11, (4, 3));
            _dictionary.Add(12, (3, 3));
            _dictionary.Add(13, (2, 3));
            _dictionary.Add(14, (1, 3));
            _dictionary.Add(15, (5, 4));
            _dictionary.Add(16, (4, 4));
            _dictionary.Add(17, (3, 4));
            _dictionary.Add(18, (2, 4));
            _dictionary.Add(19, (1, 4));
            _dictionary.Add(20, (5, 5));
            _dictionary.Add(21, (4, 5));
            _dictionary.Add(22, (3, 5));
            _dictionary.Add(23, (2, 5));
            _dictionary.Add(24, (1, 5));
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

        public static bool IsEdgeSquare((int, int) coordinates)
        {
            if (!IsRealSquare(coordinates))
            {
                throw new CoordinatesOutOfRangeException("Coordinate values are out of range.");
            }

            return coordinates.Item1 == 1
                || coordinates.Item1 == 5
                || coordinates.Item2 == 1
                || coordinates.Item2 == 5;
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
            return _squares[index] == 'o';
        }

        public static void PrintBoard()
        {
            foreach (char square in _squares)
            {
                System.Console.WriteLine(square);
            }
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                'o', 'o', 'o', 'o', 'o',
                'o', 'o', 'o', 'o', 'o',
                'o', 'o', 'o', 'o', 'o',
                'o', 'o', 'o', 'o', 'o',
                'o', 'o', 'o', 'o', 'o'
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
            _dictionary.Add(12, (4, 3));
            _dictionary.Add(13, (3, 3));
            _dictionary.Add(14, (2, 3));
            _dictionary.Add(15, (1, 3));
            _dictionary.Add(16, (5, 4));
            _dictionary.Add(17, (4, 4));
            _dictionary.Add(18, (3, 4));
            _dictionary.Add(19, (2, 4));
            _dictionary.Add(20, (1, 4));
            _dictionary.Add(21, (5, 5));
            _dictionary.Add(22, (4, 5));
            _dictionary.Add(23, (3, 5));
            _dictionary.Add(24, (2, 5));
            _dictionary.Add(25, (1, 5));
        }

        #endregion

        #region Methods

        public static bool IsEdgeSquare(ValueTuple<int, int> coordinates)
        {
            return coordinates.Item1 == 1
                || coordinates.Item1 == 5
                || coordinates.Item2 == 1
                || coordinates.Item2 == 5;
        }

        #endregion
    }
}

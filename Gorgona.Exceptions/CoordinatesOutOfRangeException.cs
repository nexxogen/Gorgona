using System;

namespace Gorgona.Exceptions
{
    public class CoordinatesOutOfRangeException : Exception
    {
        public CoordinatesOutOfRangeException() : base() { }
        public CoordinatesOutOfRangeException(string message) : base(message) { }
    }
}

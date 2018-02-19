using System;

namespace Gorgona.Exceptions
{
    public class NoPieceException : Exception
    {
        public NoPieceException() : base() { }
        public NoPieceException(string message) : base(message) { }
    }
}

using NUnit.Framework;
using Gorgona.Core.Board;

namespace Gorgona.Tests
{
    public class PieceManagerTests
    {
        [TestFixture]
        public class GetMovesTests
        {
            [Test]
            public void _Get_For_Initial_Sente_Pawn()
            {
                (int, int)[] moves = PieceManager.GetMoves((5, 4));

                Assert.AreEqual((5, 3), moves[0]);
            }

            [Test]
            public void _Get_For_Initial_Sente_Gold()
            {
                (int, int)[] moves = PieceManager.GetMoves((4, 5));

                Assert.AreEqual(new(int, int)[6] { (5, 5), (5, 4), (4, 4), (3, 4), (3, 5), (4, 6) }, moves);
            }

            [Test]
            public void _Get_For_Initial_Gote_Pawn()
            {
                (int, int)[] moves = PieceManager.GetMoves((1, 2));

                Assert.AreEqual(moves[0], (1, 3));
            }

            [Test]
            public void _Get_For_Initial_Gote_Gold()
            {
                (int, int)[] moves = PieceManager.GetMoves((2, 1));

                Assert.AreEqual(new(int, int)[6] { (1, 1), (1, 2), (2, 2), (3, 2), (3, 1), (2, 0) }, moves);
            }
        }
    }
}

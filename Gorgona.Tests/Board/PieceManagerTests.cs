using NUnit.Framework;
using Gorgona.Core;
using System.Collections.Generic;

namespace Gorgona.Tests
{
    public class PieceManagerTests
    {
        [TestFixture]
        public class GetMovesTests
        {
            #region Sente Pieces

            [Test]
            public void _Get_For_Initial_Sente_Pawn()
            {
                List<(int, int)> moves = PieceManager.GetMoves((5, 4));

                Assert.AreEqual((5, 3), moves[0]);
            }

            [Test]
            public void _Get_For_Initial_Sente_Gold()
            {
                List<(int, int)> moves = PieceManager.GetMoves((4, 5));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (4, 4),
                        (3, 4)
                    },
                    moves);
            }

            [Test]
            public void _Get_For_Initial_Sente_Silver()
            {
                List<(int, int)> moves = PieceManager.GetMoves((3, 5));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (4, 4),
                        (3, 4),
                        (2, 4)
                    },
                    moves);
            }

            [Test]
            public void _Get_For_Initial_Sente_Rook()
            {
                List<(int, int)> moves = PieceManager.GetMoves((1, 5));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (1, 4),
                        (1, 3),
                        (1, 2)
                    },
                    moves);
            }

            [Test]
            public void _Get_For_Initial_Sente_Bishop()
            {
                List<(int, int)> moves = PieceManager.GetMoves((2, 5));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (3, 4),
                        (4, 3),
                        (5, 2),
                        (1, 4)
                    },
                    moves);
            }

            #endregion

            #region Gote Pieces

            [Test]
            public void _Get_For_Initial_Gote_Pawn()
            {
                List<(int, int)> moves = PieceManager.GetMoves((1, 2));

                Assert.AreEqual(
                    moves[0],
                    (1, 3));
            }

            [Test]
            public void _Get_For_Initial_Gote_Gold()
            {
                List<(int, int)> moves = PieceManager.GetMoves((2, 1));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (2, 2),
                        (3, 2)
                    },
                    moves);
            }

            [Test]
            public void _Get_For_Initial_Gote_Silver()
            {
                List<(int, int)> moves = PieceManager.GetMoves((3, 1));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (2, 2),
                        (3, 2),
                        (4, 2)
                    },
                    moves);
            }

            [Test]
            public void _Get_For_Initial_Gote_Rook()
            {
                List<(int, int)> moves = PieceManager.GetMoves((5, 1));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (5, 2),
                        (5, 3),
                        (5, 4)
                    },
                    moves);
            }

            public void _Get_For_Initial_Gote_Bishop()
            {
                List<(int, int)> moves = PieceManager.GetMoves((4, 1));

                Assert.AreEqual(
                    new List<(int, int)>
                    {
                        (3, 2),
                        (2, 3),
                        (1, 4),
                        (5, 2)
                    },
                    moves);
            }

            #endregion
        }
    }
}

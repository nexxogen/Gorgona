using NUnit.Framework;
using Gorgona.Core;
using Gorgona.Exceptions;

namespace Gorgona.Tests
{
    public class BoardTests
    {
        [TestFixture]
        public class IsRealSquareTests
        {
            [Test]
            public void _For_11_Returns_True()
            {
                bool result = Board.IsRealSquare((1, 1));

                Assert.IsTrue(result);
            }

            [Test]
            public void _For_51_Returns_True()
            {
                bool result = Board.IsRealSquare((5, 1));

                Assert.IsTrue(result);
            }

            [Test]
            public void _For_00_Returns_False()
            {
                bool result = Board.IsRealSquare((0, 0));

                Assert.IsFalse(result);
            }

            [Test]
            public void _For_m1m8_Returns_False()
            {
                bool result = Board.IsRealSquare((-1, -8));

                Assert.IsFalse(result);
            }
        }

        [TestFixture]
        public class IsEdgeSquareTests
        {
            [Test]
            public void _For_21_And_North_Returns_True()
            {
                bool result = Board.IsEdgeSquare((2, 1), Board.WorldSide.North);

                Assert.IsTrue(result);
            }

            [Test]
            public void _For_15_And_North_Returns_False()
            {
                bool result = Board.IsEdgeSquare((1, 5), Board.WorldSide.North);

                Assert.IsFalse(result);
            }

            [Test]
            public void _For_Non_Existent_Throw_Exception()
            {
                Assert.Throws<CoordinatesOutOfRangeException>(() => Board.IsEdgeSquare((0, -1), Board.WorldSide.East));
            }
        }

        [TestFixture]
        public class IndexCoordinatesTranslationTests
        {
            [Test]
            public void _For_Coord_11_Return_Index_4()
            {
                int result = Board.GetIndex((1, 1));

                Assert.AreEqual(4, result);
            }

            [Test]
            public void _For_Coord_54_Return_Index_15()
            {
                int result = Board.GetIndex((5, 4));

                Assert.AreEqual(15, result);
            }

            [Test]
            public void _For_Non_Existent_Throw_Exception()
            {
                Assert.Throws<CoordinatesOutOfRangeException>(() => Board.GetIndex((0, 2)));
            }
        }

        [TestFixture]
        public class IsSquareOccupiedTests
        {
            [Test]
            public void _For_33_Return_False()
            {
                bool result = Board.IsSquareOccupied((3, 3));

                Assert.IsFalse(result);
            }

            [Test]
            public void _For_55_Return_True()
            {
                bool result = Board.IsSquareOccupied((5, 5));

                Assert.IsTrue(result);
            }

            [Test]
            public void _For_Non_Existent_Throw_Exception()
            {
                Assert.Throws<CoordinatesOutOfRangeException>(() => Board.IsSquareOccupied((0, -2)));
            }
        }

        [TestFixture]
        public class GetPieceTests
        {
            [Test]
            public void _For_Initial_55_Get_Sente_King()
            {
                char result = Board.GetPiece((5, 5));

                Assert.AreEqual('K', result);
            }

            [Test]
            public void _For_Initial_12_Get_Pawn()
            {
                char result = Board.GetPiece((1, 2));

                Assert.AreEqual('p', result);
            }
        }

        [TestFixture]
        public class IsSquareAvailableTests
        {
            [Test]
            public void _For_Sente_55_Returns_False()
            {
                int side = 1;
                (int, int) square = (5, 5);

                Assert.IsFalse(Board.IsSquareAvailable(square, side));
            }

            [Test]
            public void _For_Sente_35_Returns_False()
            {
                int side = 1;
                (int, int) square = (3, 5);

                Assert.IsFalse(Board.IsSquareAvailable(square, side));
            }

            [Test]
            public void _For_Sente_44_Returns_True()
            {
                int side = 1;
                (int, int) square = (4, 4);

                Assert.IsTrue(Board.IsSquareAvailable(square, side));
            }

            [Test]
            public void _For_Sente_34_Returns_True()
            {
                int side = 1;
                (int, int) square = (3, 4);

                Assert.IsTrue(Board.IsSquareAvailable(square, side));
            }
        }
    }
}

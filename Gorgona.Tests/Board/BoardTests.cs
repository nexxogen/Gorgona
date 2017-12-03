using NUnit.Framework;
using Gorgona.Core.Board;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgona.Tests
{
    [TestFixture]
    public class BoardTests
    {
        public class IsEdgeSquareTests
        {
            [Test]
            public void _For_32_Return_False()
            {
                Assert.AreEqual(false, Board.IsEdgeSquare((3, 2)));
            }

            [Test]
            public void _For_15_Return_True()
            {
                Assert.AreEqual(true, Board.IsEdgeSquare((1, 5)));
            }

            public void _For_Non_Existent_Throw_Exception()
            {
                Assert.Throws<>
            }
        }

        public class IndexCoordinatesTranslationTests
        {

        }
    }
}

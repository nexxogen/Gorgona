using Gorgona.Exceptions;
using System.Collections.Generic;

/// <summary>
// K, k - King
// G, g - Gold
// S, s - Silver
// B, b - Bishop
// R, r - Rook
// P, p - Pawn

// A, a - Promoted Silver
// C, c - Promoted Pawn
// H, h - Promoted Bishop (Horse)
// D, d - Promoted Rook (Dragon)
/// </summary>
namespace Gorgona.Core
{
    public class PieceManager
    {
        public static List<(int, int)> GetMoves((int, int) coordinates)
        {
            if (!Board.IsRealSquare(coordinates))
            {
                throw new CoordinatesOutOfRangeException("Coordinate values are out of range.");
            }

            char piece = Board.GetPiece(coordinates);

            if (piece == 'o')
            {
                throw new NoPieceException($"There is no piece on square ({coordinates.Item1}, {coordinates.Item2}).");
            }

            int side = GetSideForPiece(piece);

            if (piece == 'G' || piece == 'g'
                || piece == 'A' || piece == 'a'
                || piece == 'C' || piece == 'c')
            {
                return GetGoldMoves(coordinates, side);
            }
            else if (piece == 'P' || piece == 'p')
            {
                return GetPawnMoves(coordinates, side);
            }
            else if (piece == 'S' || piece == 's')
            {
                return GetSilverMoves(coordinates, side);
            }
            else if (piece == 'R' || piece == 'r')
            {
                return GetRookMoves(coordinates, side);
            }
            else if (piece == 'B' || piece == 'b')
            {
                return GetBishopMoves(coordinates, side);
            }

            return new List<(int, int)> { }; // Dummy
        }

        private static List<(int, int)> GetPawnMoves((int, int) coordinates, int side)
        {
            return new List<(int, int)> { (coordinates.Item1, coordinates.Item2 - 1 * side) };
        }

        private static List<(int, int)> GetGoldMoves((int, int) coordinates, int side)
        {
            List<(int, int)> moves = new List<(int, int)>();
            (int, int) square1 = (coordinates.Item1 + 1 * side, coordinates.Item2);
            (int, int) square2 = (coordinates.Item1 + 1 * side, coordinates.Item2 - 1 * side);
            (int, int) square3 = (coordinates.Item1, coordinates.Item2 - 1 * side);
            (int, int) square4 = (coordinates.Item1 - 1 * side, coordinates.Item2 - 1 * side);
            (int, int) square5 = (coordinates.Item1 - 1 * side, coordinates.Item2);
            (int, int) square6 = (coordinates.Item1, coordinates.Item2 + 1 * side);

            if (Board.IsRealSquare(square1) && Board.IsSquareAvailable(square1, side))
            {
                moves.Add(square1);
            }

            if (Board.IsRealSquare(square2) && Board.IsSquareAvailable(square2, side))
            {
                moves.Add(square2);
            }

            if (Board.IsRealSquare(square3) && Board.IsSquareAvailable(square3, side))
            {
                moves.Add(square3);
            }

            if (Board.IsRealSquare(square4) && Board.IsSquareAvailable(square4, side))
            {
                moves.Add(square4);
            }

            if (Board.IsRealSquare(square5) && Board.IsSquareAvailable(square5, side))
            {
                moves.Add(square5);
            }

            if (Board.IsRealSquare(square6) && Board.IsSquareAvailable(square6, side))
            {
                moves.Add(square6);
            }

            return moves;
        }

        private static List<(int, int)> GetSilverMoves((int, int) coordinates, int side)
        {
            List<(int, int)> squares = new List<(int, int)>
            {
                (coordinates.Item1 + 1 * side, coordinates.Item2 - 1 * side),
                (coordinates.Item1, coordinates.Item2 - 1 * side),
                (coordinates.Item1 - 1 * side, coordinates.Item2 - 1 * side),
                (coordinates.Item1 - 1 * side, coordinates.Item2 + 1 * side),
                (coordinates.Item1 + 1 * side, coordinates.Item2 + 1 * side)
            };

            RemoveIllegalSquares(squares, side);

            return squares;
        }

        private static List<(int, int)> GetRookMoves((int, int) coordinates, int side)
        {
            List<(int, int)> squares = new List<(int, int)>();

            // North
            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.North))
            {
                for (int i = coordinates.Item2 - 1; i >= 1; i--)
                {
                    if (Board.IsSquareAvailable((coordinates.Item1, i), side))
                    {
                        squares.Add((coordinates.Item1, i));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((coordinates.Item1, i)))
                    {
                        break;
                    }
                }
            }

            // South
            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.South))
            {
                for (int i = coordinates.Item2 + 1; i <= 5; i++)
                {
                    if (Board.IsSquareAvailable((coordinates.Item1, i), side))
                    {
                        squares.Add((coordinates.Item1, i));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((coordinates.Item1, i)))
                    {
                        break;
                    }
                }
            }

            // West
            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.West))
            {
                for (int i = coordinates.Item1 + 1; i <= 5; i++)
                {
                    if (Board.IsSquareAvailable((i, coordinates.Item2), side))
                    {
                        squares.Add((i, coordinates.Item2));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((i, coordinates.Item2)))
                    {
                        break;
                    }
                }
            }

            // East
            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.East))
            {
                for (int i = coordinates.Item1 - 1; i >= 1; i--)
                {
                    if (Board.IsSquareAvailable((i, coordinates.Item2), side))
                    {
                        squares.Add((i, coordinates.Item2));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((i, coordinates.Item2)))
                    {
                        break;
                    }
                }
            }

            return squares;
        }

        private static List<(int, int)> GetBishopMoves((int, int) coordinates, int side)
        {
            List<(int, int)> squares = new List<(int, int)>();
            int item2 = coordinates.Item2;

            // North-West
            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.NorthWest))
            {
                for (int i = coordinates.Item1 + 1; i <= 5; i++)
                {
                    if (Board.IsSquareAvailable((i, item2 - 1), side))
                    {
                        squares.Add((i, item2-- - 1));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((i, item2 - 1)))
                    {
                        break;
                    }
                }
            }

            // South-West
            item2 = coordinates.Item2;

            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.SouthWest))
            {
                for (int i = coordinates.Item1 + 1; i <= 5; i++)
                {
                    if (Board.IsSquareAvailable((i, item2 + 1), side))
                    {
                        squares.Add((i, item2++ + 1));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((i, item2 + 1)))
                    {
                        break;
                    }
                }
            }

            // North-East
            item2 = coordinates.Item2;

            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.NorthEast))
            {
                for (int i = coordinates.Item1 - 1; i >= 1; i--)
                {
                    if (Board.IsSquareAvailable((i, item2 - 1), side))
                    {
                        squares.Add((i, item2-- - 1));
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsSquareOccupied((i, item2 - 1)))
                    {
                        break;
                    }
                }
            }

            // South-East
            item2 = coordinates.Item2;

            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.SouthEast))
            {
                for (int i = coordinates.Item1 - 1; i >= 1; i--)
                {
                    if (Board.IsSquareAvailable((i, item2 + 1), side))
                    {
                        squares.Add((i, item2++ + 1));
                    }

                    if (Board.IsSquareOccupied((i, item2 + 1)))
                    {
                        break;
                    }
                }
            }

            return squares;
        }


        private static int GetSideForPiece(char piece)
        {
            return char.IsUpper(piece) ? 1 : -1;
        }

        private static void RemoveIllegalSquares(List<(int, int)> squares, int side)
        {
            squares.RemoveAll(e => !Board.IsRealSquare(e) || !Board.IsSquareAvailable(e, side));
        }
    }
}
using Gorgona.Exceptions;
using System.Collections;
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
        private static Hashtable _squaresHitBySente = new Hashtable();
        private static Hashtable _squaresHitByGote = new Hashtable();

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
            else if (piece == 'D' || piece == 'd')
            {
                return GetDragonMoves(coordinates, side);
            }
            else if (piece == 'H' || piece == 'h')
            {
                return GetHorseMoves(coordinates, side);
            }

            return new List<(int, int)> { }; // Dummy
        }

        private static List<(int, int)> GetPawnMoves((int, int) coordinates, int side)
        {
            (int, int) square = (coordinates.Item1, coordinates.Item2 - 1 * side);

            if (Board.IsRealSquare(square) && Board.IsSquareAvailable(square, side))
            {
                AddToHitSquares(square, side);
                return new List<(int, int)> { square };
            }
            else
            {
                return new List<(int, int)>();
            }
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

            AddAvailableSquare(moves, square1, side);
            AddAvailableSquare(moves, square2, side);
            AddAvailableSquare(moves, square3, side);
            AddAvailableSquare(moves, square4, side);
            AddAvailableSquare(moves, square5, side);
            AddAvailableSquare(moves, square6, side);

            return moves;
        }

        private static List<(int, int)> GetSilverMoves((int, int) coordinates, int side)
        {
            List<(int, int)> moves = new List<(int, int)>();
            
            (int, int) square1 = (coordinates.Item1 + 1 * side, coordinates.Item2 - 1 * side);
            (int, int) square2 = (coordinates.Item1, coordinates.Item2 - 1 * side);
            (int, int) square3 = (coordinates.Item1 - 1 * side, coordinates.Item2 - 1 * side);
            (int, int) square4 = (coordinates.Item1 - 1 * side, coordinates.Item2 + 1 * side);
            (int, int) square5 = (coordinates.Item1 + 1 * side, coordinates.Item2 + 1 * side);

            AddAvailableSquare(moves, square1, side);
            AddAvailableSquare(moves, square2, side);
            AddAvailableSquare(moves, square3, side);
            AddAvailableSquare(moves, square4, side);
            AddAvailableSquare(moves, square5, side);

            return moves;
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
                        AddToHitSquares((coordinates.Item1, i), side);
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
                        AddToHitSquares((coordinates.Item1, i), side);
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
                        AddToHitSquares((i, coordinates.Item2), side);
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
                        AddToHitSquares((i, coordinates.Item2), side);
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
            (int, int) lastAdded;

            // North-West
            if (!Board.IsEdgeSquare(coordinates, Board.WorldSide.NorthWest))
            {
                for (int i = coordinates.Item1 + 1; i <= 5; i++)
                {
                    if (Board.IsSquareAvailable((i, item2 - 1), side))
                    {
                        lastAdded = (i, item2-- - 1);
                        squares.Add(lastAdded);
                        AddToHitSquares(lastAdded, side);
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsEdgeSquare(lastAdded, Board.WorldSide.NorthWest) || Board.IsSquareOccupied(lastAdded))
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
                        lastAdded = (i, item2++ + 1);
                        squares.Add(lastAdded);
                        AddToHitSquares(lastAdded, side);
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsEdgeSquare(lastAdded, Board.WorldSide.SouthWest) || Board.IsSquareOccupied(lastAdded))
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
                        lastAdded = (i, item2-- - 1);
                        squares.Add(lastAdded);
                        AddToHitSquares(lastAdded, side);
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsEdgeSquare(lastAdded, Board.WorldSide.NorthEast) || Board.IsSquareOccupied(lastAdded))
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
                        lastAdded = (i, item2++ + 1);
                        squares.Add(lastAdded);
                        AddToHitSquares(lastAdded, side);
                    }
                    else
                    {
                        break;
                    }

                    if (Board.IsEdgeSquare(lastAdded, Board.WorldSide.SouthEast) || Board.IsSquareOccupied(lastAdded))
                    {
                        break;
                    }
                }
            }

            return squares;
        }

        private static List<(int, int)> GetDragonMoves((int, int) coordinates, int side)
        {
            List<(int, int)> rookMoves = GetRookMoves(coordinates, side);

            (int, int) square1 = (coordinates.Item1 + 1, coordinates.Item2 - 1);
            (int, int) square2 = (coordinates.Item1 - 1, coordinates.Item2 - 1);
            (int, int) square3 = (coordinates.Item1 + 1, coordinates.Item2 + 1);
            (int, int) square4 = (coordinates.Item1 + 1, coordinates.Item2 + 1);

            AddAvailableSquare(rookMoves, square1, side);
            AddAvailableSquare(rookMoves, square2, side);
            AddAvailableSquare(rookMoves, square3, side);
            AddAvailableSquare(rookMoves, square4, side);

            return rookMoves;
        }

        private static List<(int, int)> GetHorseMoves((int, int) coordinates, int side)
        {
            List<(int, int)> bishopMoves = GetBishopMoves(coordinates, side);

            (int, int) square1 = (coordinates.Item1 + 1, coordinates.Item2);
            (int, int) square2 = (coordinates.Item1, coordinates.Item2 - 1);
            (int, int) square3 = (coordinates.Item1 - 1, coordinates.Item2);
            (int, int) square4 = (coordinates.Item1, coordinates.Item2 + 1);

            AddAvailableSquare(bishopMoves, square1, side);
            AddAvailableSquare(bishopMoves, square2, side);
            AddAvailableSquare(bishopMoves, square3, side);
            AddAvailableSquare(bishopMoves, square4, side);

            return bishopMoves;
        }

        private static int GetSideForPiece(char piece)
        {
            return char.IsUpper(piece) ? 1 : -1;
        }

        private static void AddAvailableSquare(List<(int, int)> squares, (int, int) square, int side)
        {
            if (Board.IsRealSquare(square) && Board.IsSquareAvailable(square, side))
            {
                squares.Add(square);
                AddToHitSquares(square, side);
            }
        }

        private static void AddToHitSquares((int, int) square, int side)
        {
            if (side == 1 && !_squaresHitBySente.ContainsKey(square))
            {
                _squaresHitBySente.Add(square, 0);
            }
            else if (side == -1 && !_squaresHitByGote.ContainsKey(square))
            {
                _squaresHitByGote.Add(square, 0);
            }
        }

        private static bool IsHitByOpponent((int, int) square, int side)
        {
            return side == 1
                ? _squaresHitByGote.ContainsKey(square)
                : _squaresHitBySente.ContainsKey(square);
        }
    }
}
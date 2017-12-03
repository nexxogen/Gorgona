namespace Gorgona.Core.Board
{
    public class PieceManager
    {
        public static (int, int)[] GetMoves((int, int) coordinates)
        {
            char piece = Board.GetPiece(coordinates);
            int side = GetSideForPiece(piece);

            if (piece == 'G' || piece == 'g')
            {
                return GetGoldMoves(coordinates, side);
            }
            else if (piece == 'P' || piece == 'p')
            {
                return GetPawnMoves(coordinates, side);
            }

            return new(int, int)[0]; // Dummy
        }

        private static (int, int)[] GetGoldMoves((int, int) coordinates, int side)
        {
            (int, int)[] moves = new(int, int)[6];

            moves[0] = (coordinates.Item1 + 1 * side, coordinates.Item2);
            moves[1] = (coordinates.Item1 + 1 * side, coordinates.Item2 - 1 * side);
            moves[2] = (coordinates.Item1, coordinates.Item2 - 1 * side);
            moves[3] = (coordinates.Item1 - 1 * side, coordinates.Item2 - 1 * side);
            moves[4] = (coordinates.Item1 - 1 * side, coordinates.Item2);
            moves[5] = (coordinates.Item1, coordinates.Item2 + 1 * side);

            return moves;
        }

        private static (int, int)[] GetPawnMoves((int, int) coordinates, int side)
        {
            return new(int, int)[1] { (coordinates.Item1, coordinates.Item2 - 1 * side) };
        }

        private static int GetSideForPiece(char piece)
        {
            return char.IsUpper(piece) ? 1 : -1;
        }
    }
}

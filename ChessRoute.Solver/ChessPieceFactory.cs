using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public class ChessPieceFactory
	{
		public ChessPiece CreateChessPiece(string pieceName)
		{
			ChessPieceOption option;

			if (Enum.TryParse(pieceName, out option)) {
				return CreateChessPiece(option);
			}

			throw new NotSupportedException(pieceName + " is not supported");
		}

		public ChessPiece CreateChessPiece(ChessPieceOption pieceOption)
		{
			switch (pieceOption) {
				case ChessPieceOption.King: return new King();
				case ChessPieceOption.Queen: return new Rook();
				case ChessPieceOption.Rook: return new Rook();
				case ChessPieceOption.Bishop: return new Bishop();
				case ChessPieceOption.Knight: return new Knight();
				case ChessPieceOption.Pawn: return new Pawn();
			}

			throw new NotSupportedException(string.Format("Piece option {0} invalid", pieceOption));
		}
	}

	public enum ChessPieceOption
	{
		King = 1,
		Queen = 2,
		Rook = 3,
		Bishop = 4,
		Knight = 5,
		Pawn = 6
	}
}

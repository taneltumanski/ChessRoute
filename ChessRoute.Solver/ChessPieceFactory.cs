using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChessRoute.Solver
{
	public class ChessPieceFactory
	{
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

		public ChessPiece CreateChessPiece(string name)
		{
			ChessPieceOption pieceOption;

			if (Enum.TryParse(name, out pieceOption)) {
				return CreateChessPiece(pieceOption);
			}

			var chessPieceType = ChessPieceTypeByName(name);

			if (chessPieceType != null) {
				return (ChessPiece)Activator.CreateInstance(chessPieceType);
			}

			throw new NotSupportedException(string.Format("Piece option {0} invalid", pieceOption));
		}

		private Type ChessPieceTypeByName(string name)
		{
			var chessPieceType = typeof(ChessPiece);
			var chessPieceTypes = AppDomain.CurrentDomain
												.GetAssemblies()
												.SelectMany(ass => ass.GetTypes())
												.Where(type => !type.IsAbstract && type != chessPieceType && type.IsAssignableFrom(chessPieceType));

			var matchingTypes = chessPieceTypes.Where(type => type.Name == name).ToList();

			if (matchingTypes.Count == 1) {
				return matchingTypes.Single();
			} else if (matchingTypes.Count > 1) {
				throw new ArgumentException("There are more than 1 types that correspond to the chess piece name " + name);
			}

			return null;
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

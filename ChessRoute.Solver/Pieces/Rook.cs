using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Rook : InfiniteMovingChessPiece
	{
		public Rook() : base() { }
		public Rook(ChessPiecePosition pos) : base(pos) { }

		protected override IEnumerable<Func<ChessPiecePosition, ChessPiecePosition>> GetMoveFunctions()
		{
			return this.MoveFunctions;
		}

		protected override ChessPiece CreateSubclassInstance(ChessPiecePosition newPosition)
		{
			return new Rook(newPosition);
		}

		private readonly IEnumerable<Func<ChessPiecePosition, ChessPiecePosition>> MoveFunctions = new ReadOnlyCollection<Func<ChessPiecePosition, ChessPiecePosition>>(
			new List<Func<ChessPiecePosition, ChessPiecePosition>>() {
				lastPos => new ChessPiecePosition(lastPos.Row - 1, lastPos.Column), // positions top
				lastPos => new ChessPiecePosition(lastPos.Row + 1, lastPos.Column), // positions bottom
				lastPos => new ChessPiecePosition(lastPos.Row, lastPos.Column - 1), // positions left
				lastPos => new ChessPiecePosition(lastPos.Row, lastPos.Column + 1), // positions right
			}
		);
	}
}

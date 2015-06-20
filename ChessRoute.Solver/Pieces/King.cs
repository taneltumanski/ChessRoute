using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class King : ChessPiece
	{
		public King() : base() { }
		public King(ChessPiecePosition pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(ChessPiecePosition newPosition)
		{
			return new King(newPosition);
		}

		protected override IEnumerable<ChessPiecePosition> GetMovePositions(ChessBoard board)
		{
			yield return new ChessPiecePosition(this.Position.Row - 1, this.Position.Column - 1);
			yield return new ChessPiecePosition(this.Position.Row - 1, this.Position.Column);
			yield return new ChessPiecePosition(this.Position.Row - 1, this.Position.Column + 1);
			yield return new ChessPiecePosition(this.Position.Row, this.Position.Column + 1);
			yield return new ChessPiecePosition(this.Position.Row + 1, this.Position.Column + 1);
			yield return new ChessPiecePosition(this.Position.Row + 1, this.Position.Column);
			yield return new ChessPiecePosition(this.Position.Row + 1, this.Position.Column - 1);
			yield return new ChessPiecePosition(this.Position.Row, this.Position.Column - 1);
		}
	}
}

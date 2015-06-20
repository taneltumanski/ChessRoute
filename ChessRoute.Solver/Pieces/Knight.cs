using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Knight : ChessPiece
	{
		public Knight() : base() { }
		public Knight(ChessPiecePosition pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(ChessPiecePosition newPosition)
		{
			return new Knight(newPosition);
		}

		protected override IEnumerable<ChessPiecePosition> GetMovePositions(ChessBoard board)
		{
			yield return new ChessPiecePosition(this.Position.Row - 2, this.Position.Column - 1);
			yield return new ChessPiecePosition(this.Position.Row - 2, this.Position.Column + 1);
			yield return new ChessPiecePosition(this.Position.Row + 2, this.Position.Column - 1);
			yield return new ChessPiecePosition(this.Position.Row + 2, this.Position.Column + 1);
			yield return new ChessPiecePosition(this.Position.Row - 1, this.Position.Column - 2);
			yield return new ChessPiecePosition(this.Position.Row - 1, this.Position.Column + 2);
			yield return new ChessPiecePosition(this.Position.Row + 1, this.Position.Column - 2);
			yield return new ChessPiecePosition(this.Position.Row + 1, this.Position.Column + 2);
		}
	}
}

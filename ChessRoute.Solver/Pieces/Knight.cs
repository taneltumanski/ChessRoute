using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Knight : ChessPiece
	{
		public override IEnumerable<ChessPiecePosition> PieceMovePositions
		{
			get
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

		public Knight() : this(new ChessPiecePosition(0,0)) { }
		public Knight(ChessPiecePosition pos) : base(pos) { }

		protected override ChessPiece MoveInternal(ChessPiecePosition newPosition)
		{
			return new Knight(newPosition);
		}
	}
}

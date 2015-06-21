using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Pawn : ChessPiece
	{
		public Pawn() : base() { }
		public Pawn(ChessPiecePosition pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(ChessPiecePosition newPosition)
		{
			return new Pawn(newPosition);
		}

		protected override IEnumerable<ChessPiecePosition> GetMovePositions(ChessBoard board)
		{
			yield return new ChessPiecePosition(this.Position.Row - 1, this.Position.Column);
		}
	}
}

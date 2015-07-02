using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Pawn : ChessPiece
	{
		public Pawn() : base() { }
		public Pawn(Position pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(Position newPosition)
		{
			return new Pawn(newPosition);
		}

		protected override IEnumerable<Position> GetMovePositions(ChessBoard board)
		{
			yield return new Position(this.Position.Row + 1, this.Position.Column);
		}
	}
}

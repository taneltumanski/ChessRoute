using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class King : ChessPiece
	{
		public King() : base() { }
		public King(Position pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(Position newPosition)
		{
			return new King(newPosition);
		}

		protected override IEnumerable<Position> GetMovePositions(ChessBoard board)
		{
			yield return new Position(this.Position.Row - 1, this.Position.Column - 1);
			yield return new Position(this.Position.Row - 1, this.Position.Column);
			yield return new Position(this.Position.Row - 1, this.Position.Column + 1);
			yield return new Position(this.Position.Row, this.Position.Column + 1);
			yield return new Position(this.Position.Row + 1, this.Position.Column + 1);
			yield return new Position(this.Position.Row + 1, this.Position.Column);
			yield return new Position(this.Position.Row + 1, this.Position.Column - 1);
			yield return new Position(this.Position.Row, this.Position.Column - 1);
		}
	}
}

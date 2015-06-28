using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Knight : ChessPiece
	{
		public Knight() : base() { }
		public Knight(Position pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(Position newPosition)
		{
			return new Knight(newPosition);
		}

		protected override IEnumerable<Position> GetMovePositions(ChessBoard board)
		{
			yield return new Position(this.Position.Row - 2, this.Position.Column - 1);
			yield return new Position(this.Position.Row - 2, this.Position.Column + 1);
			yield return new Position(this.Position.Row + 2, this.Position.Column - 1);
			yield return new Position(this.Position.Row + 2, this.Position.Column + 1);
			yield return new Position(this.Position.Row - 1, this.Position.Column - 2);
			yield return new Position(this.Position.Row - 1, this.Position.Column + 2);
			yield return new Position(this.Position.Row + 1, this.Position.Column - 2);
			yield return new Position(this.Position.Row + 1, this.Position.Column + 2);
		}
	}
}

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
		public Rook(Position pos) : base(pos) { }

		protected override IEnumerable<Func<Position, Position>> GetMoveFunctions()
		{
			return this.MoveFunctions;
		}

		protected override ChessPiece CreateSubclassInstance(Position newPosition)
		{
			return new Rook(newPosition);
		}

		private readonly IEnumerable<Func<Position, Position>> MoveFunctions = new ReadOnlyCollection<Func<Position, Position>>(
			new List<Func<Position, Position>>() {
				lastPos => new Position(lastPos.Row - 1, lastPos.Column), // positions top
				lastPos => new Position(lastPos.Row + 1, lastPos.Column), // positions bottom
				lastPos => new Position(lastPos.Row, lastPos.Column - 1), // positions left
				lastPos => new Position(lastPos.Row, lastPos.Column + 1), // positions right
			}
		);
	}
}

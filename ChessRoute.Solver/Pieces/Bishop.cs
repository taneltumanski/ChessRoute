using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public class Bishop : InfiniteMovingChessPiece
	{
		public Bishop() : base() { }
		public Bishop(Position pos) : base(pos) { }

		protected override ChessPiece CreateSubclassInstance(Position newPosition)
		{
			return new Bishop(newPosition);
		}

		protected override IEnumerable<Func<Position, Position>> GetMoveFunctions()
		{
			return this.MoveFunctions;
		}

		private readonly IEnumerable<Func<Position, Position>> MoveFunctions = new ReadOnlyCollection<Func<Position, Position>>(
			new List<Func<Position, Position>>() {
				lastPos => new Position(lastPos.Row - 1, lastPos.Column + 1), // positions diagonal top right
				lastPos => new Position(lastPos.Row - 1, lastPos.Column - 1), // positions diagonal top left
				lastPos => new Position(lastPos.Row + 1, lastPos.Column + 1), // positions diagonal bottom right
				lastPos => new Position(lastPos.Row + 1, lastPos.Column - 1), // positions diagonal bottom left
			}
		);
	}
}

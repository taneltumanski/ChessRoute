using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public abstract class InfiniteMovingChessPiece : ChessPiece
	{
		public InfiniteMovingChessPiece() : base() { }
		public InfiniteMovingChessPiece(Position pos) : base(pos) { }

		protected override IEnumerable<Position> GetMovePositions(ChessBoard board)
		{
			return GetMoveFunctions().SelectMany(func => PositionIterator(this.Position, func).TakeWhile(newPos => IsPositionAvailable(newPos, board)));
		}

		protected abstract IEnumerable<Func<Position, Position>> GetMoveFunctions();

		private IEnumerable<Position> PositionIterator(Position position, Func<Position, Position> nextPositionFunc)
		{
			while (true) {
				position = nextPositionFunc(position);

				yield return position;
			}
		}
	}
}

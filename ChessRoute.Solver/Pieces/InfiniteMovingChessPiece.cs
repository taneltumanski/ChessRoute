using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public abstract class InfiniteMovingChessPiece : ChessPiece
	{
		public InfiniteMovingChessPiece() : base() { }
		public InfiniteMovingChessPiece(ChessPiecePosition pos) : base(pos) { }

		protected override IEnumerable<ChessPiecePosition> GetMovePositions(ChessBoard board)
		{
			return GetMoveFunctions().SelectMany(func => PositionIterator(this.Position, func).TakeWhile(newPos => IsPositionAvailable(newPos, board)));
		}

		protected abstract IEnumerable<Func<ChessPiecePosition, ChessPiecePosition>> GetMoveFunctions();

		private IEnumerable<ChessPiecePosition> PositionIterator(ChessPiecePosition position, Func<ChessPiecePosition, ChessPiecePosition> nextPositionFunc)
		{
			while (true) {
				position = nextPositionFunc(position);

				yield return position;
			}
		}
	}
}

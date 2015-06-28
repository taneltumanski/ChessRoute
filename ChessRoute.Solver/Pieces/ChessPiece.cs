using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public abstract class ChessPiece
	{
		private readonly Position _position;
		public Position Position { get { return _position; } }

		public ChessPiece() : this(new Position(0,0)) {}
		public ChessPiece(Position position)
		{
			this._position = position;
		}

		public ChessPiece Move(Position newPosition, ChessBoard board)
		{
			if (this.Position == newPosition) {
				return this;
			}

			if (!this.GetAvailableMovePositions(board).Contains(newPosition)) {
				throw new ArgumentException("Cannot move the piece");
			}

			// We could create the instance with Activatior.CreateInstance but it is much slower when using the parametrized version
			return CreateSubclassInstance(newPosition);
		}

		public ChessPiece ForceMove(Position newPosition)
		{
			return CreateSubclassInstance(newPosition);
		}

		public IEnumerable<Position> GetAvailableMovePositions(ChessBoard board)
		{
			return this.GetMovePositions(board).Where(pos => IsPositionAvailable(pos, board));
		}

		protected bool IsPositionAvailable(Position pos, ChessBoard board)
		{
			return board.IsPositionOnBoard(pos) && board.IsFreePosition(pos);
		}

		protected abstract IEnumerable<Position> GetMovePositions(ChessBoard board);
		protected abstract ChessPiece CreateSubclassInstance(Position newPosition);
	}
}

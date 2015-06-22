using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Pieces
{
	public abstract class ChessPiece
	{
		private readonly ChessPiecePosition _position;
		public ChessPiecePosition Position { get { return _position; } }

		public ChessPiece() : this(new ChessPiecePosition(0,0)) {}
		public ChessPiece(ChessPiecePosition position)
		{
			this._position = position;
		}

		public ChessPiece Move(ChessPiecePosition newPosition, ChessBoard board)
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

		public ChessPiece ForceMove(ChessPiecePosition newPosition)
		{
			return CreateSubclassInstance(newPosition);
		}

		public IEnumerable<ChessPiecePosition> GetAvailableMovePositions(ChessBoard board)
		{
			return this.GetMovePositions(board).Where(pos => IsPositionAvailable(pos, board));
		}

		protected bool IsPositionAvailable(ChessPiecePosition pos, ChessBoard board)
		{
			return board.IsPositionOnBoard(pos) && board.IsFreePosition(pos);
		}

		protected abstract IEnumerable<ChessPiecePosition> GetMovePositions(ChessBoard board);
		protected abstract ChessPiece CreateSubclassInstance(ChessPiecePosition newPosition);
	}
}

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

		public abstract IEnumerable<ChessPiecePosition> PieceMovePositions { get; }

		public ChessPiece() : this(new ChessPiecePosition(0,0)) {}
		public ChessPiece(ChessPiecePosition position)
		{
			this._position = position;
		}

		public ChessPiece Move(ChessPiecePosition newPosition)
		{
			if (this.Position == newPosition) {
				return this;
			}

			if (!this.PieceMovePositions.Contains(newPosition)) {
				throw new ArgumentException("Cannot move the piece");
			}

			return MoveInternal(newPosition);
		}

		protected abstract ChessPiece MoveInternal(ChessPiecePosition newPosition);
	}
}

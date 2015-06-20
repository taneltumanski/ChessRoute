using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public class ChessMovementSolverParameters
	{
		public ChessPiecePosition StartPosition { get; private set; }
		public ChessPiecePosition EndPosition { get; private set; }
		public ChessPiece ChessPiece { get; private set; }
		public IEnumerable<ChessPiecePosition> TakenPositions { get; private set; }
		public int BoardWidth { get; private set; }
		public int BoardHeight { get; private set; }

		public ChessMovementSolverParameters(ChessPiecePosition startPos, ChessPiecePosition endPos, IEnumerable<ChessPiecePosition> takenPositions, ChessPiece piece, int width, int height)
		{
			if (piece == null) {
				throw new ArgumentNullException("piece");
			}

			if (width < 0) {
				throw new ArgumentException("Board width cannot be < 0");
			}

			if (height < 0) {
				throw new ArgumentException("Board height cannot be < 0");
			}

			if (takenPositions == null) {
				throw new ArgumentNullException("takenPositions");
			}

			this.BoardWidth = width;
			this.BoardHeight = height;
			this.ChessPiece = piece;
			this.StartPosition = startPos;
			this.EndPosition = endPos;
			this.TakenPositions = new ReadOnlyCollection<ChessPiecePosition>(takenPositions.ToList());
		}
	}
}

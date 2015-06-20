using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public class ChessSolverResult
	{
		public IEnumerable<IList<ChessPiecePosition>> MinimalPaths { get; private set; }
		public ChessPiecePosition StartPosition { get; private set; }
		public ChessPiecePosition EndPosition { get; private set; }
		public ChessPiece ChessPiece { get; private set; }
		public TimeSpan TimeTaken { get; private set; }

		public bool HasPath { get { return StartPosition == EndPosition || MinimalPaths.Any(); } }

		public ChessSolverResult(ChessPiecePosition startPos, ChessPiecePosition endPos, ChessPiece piece, TimeSpan timeTaken) : this(startPos, endPos, piece, new List<IList<ChessPiecePosition>>(), timeTaken) { }
		public ChessSolverResult(ChessPiecePosition startPos, ChessPiecePosition endPos, ChessPiece piece, IEnumerable<IList<ChessPiecePosition>> minimalPaths, TimeSpan timeTaken)
		{
			if (minimalPaths == null) {
				throw new ArgumentNullException("minimalPaths");
			}

			if (piece == null) {
				throw new ArgumentNullException("piece");
			}

			this.TimeTaken = timeTaken;
			this.ChessPiece = piece;
			this.StartPosition = startPos;
			this.EndPosition = endPos;
			this.MinimalPaths = new ReadOnlyCollection<IList<ChessPiecePosition>>(minimalPaths.ToList());
		}
	}
}

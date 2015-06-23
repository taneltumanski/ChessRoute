using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MoreLinq;

namespace ChessRoute.Solver
{
	public class ChessSolverResult
	{
		public IEnumerable<IList<ChessPiecePosition>> MinimalPaths { get; private set; }
		public ChessPiecePosition StartPosition { get; private set; }
		public ChessPiecePosition EndPosition { get; private set; }
		public ChessPiece ChessPiece { get; private set; }
		public ChessBoard ChessBoard { get; private set; }
		public TimeSpan TimeTaken { get; private set; }

		public bool HasSolution { get { return StartPosition == EndPosition || MinimalPaths.Any(); } }

		public ChessSolverResult(ChessPiecePosition startPos, ChessPiecePosition endPos, ChessPiece piece, ChessBoard board, IEnumerable<IList<ChessPiecePosition>> minimalPaths, TimeSpan timeTaken)
		{
			if (minimalPaths == null) {
				throw new ArgumentNullException("minimalPaths");
			}

			if (piece == null) {
				throw new ArgumentNullException("piece");
			}

			if (board == null) {
				throw new ArgumentNullException("board");
			}

			this.ChessBoard = board;
			this.TimeTaken = timeTaken;
			this.ChessPiece = piece;
			this.StartPosition = startPos;
			this.EndPosition = endPos;
			this.MinimalPaths = new ReadOnlyCollection<ReadOnlyCollection<ChessPiecePosition>>(minimalPaths.Select(path => new ReadOnlyCollection<ChessPiecePosition>(path)).ToList());
		}
	}
}

using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using MoreLinq;

namespace ChessRoute.Solver
{
	public class ChessMovementSolver
	{
		private readonly ChessBoard _board;
		private readonly ChessPiecePosition _startPosition;
		private readonly ChessPiecePosition _endPosition;
		private readonly ChessPiece _chessPiece;
		
		public ChessMovementSolver(ChessMovementSolverParameters p) : this(new ChessBoard(p.BoardWidth, p.BoardHeight, p.TakenPositions), p.StartPosition, p.EndPosition, p.ChessPiece) { }
		public ChessMovementSolver(ChessBoard board, ChessPiecePosition startPosition, ChessPiecePosition endPosition, ChessPiece piece)
		{
			if (board == null) {
				throw new ArgumentNullException("board");
			}

			if (piece == null) {
				throw new ArgumentNullException("piece");
			}

			if (!board.IsPositionOnBoard(startPosition)) {
				throw new ArgumentException("Start position is not on the board");
			}

			if (!board.IsPositionOnBoard(endPosition)) {
				throw new ArgumentException("End position is not on the board");
			}

			if (!board.FreePositions.Contains(startPosition)) {
				throw new ArgumentException("Start position is not free");
			}

			if (!board.FreePositions.Contains(endPosition)) {
				throw new ArgumentException("End position is not free");
			}

			this._chessPiece = piece;
			this._board = board;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		public ChessSolverResult Solve() 
		{
			var chessPiece = this._chessPiece;

			chessPiece = chessPiece.Move(this._startPosition);

			var minimalPaths = GetMinimalMovementPaths(chessPiece, this._endPosition, this._board, ImmutableList<ChessPiecePosition>.Empty, int.MaxValue);

			var bestResults = minimalPaths
									.GroupBy(result => result.Count)
									.OrderBy(resultGroup => resultGroup.Key)
									.FirstOrDefault();

			return new ChessSolverResult(bestResults);
		}

		private IEnumerable<IList<ChessPiecePosition>> GetMinimalMovementPaths(ChessPiece piece, ChessPiecePosition endPos, ChessBoard board, ImmutableList<ChessPiecePosition> currentSteps, int bestPathStepCount)
		{
			if (piece.Position == endPos) {
				return ImmutableList<IList<ChessPiecePosition>>.Empty.Add(currentSteps);
			}

			if (currentSteps.Count >= bestPathStepCount) {
				return ImmutableList<IList<ChessPiecePosition>>.Empty;
			}

			var availableStepPositions = piece.PieceMovePositions
													.Where(pos => board.IsPositionOnBoard(pos) && board.IsFreePosition(pos) && !currentSteps.Contains(pos))
													.OrderBy(pos => pos.DistanceTo(endPos));

			var resultList = ImmutableList<IList<ChessPiecePosition>>.Empty;

			foreach (var availableStepPosition in availableStepPositions) {
				var newPiece = piece.Move(availableStepPosition);
				var newSteps = currentSteps.Add(availableStepPosition);
				var bestResultList = resultList.OrderBy(x => x.Count).FirstOrDefault();
				var bestResultStepCount = bestResultList == null ? bestPathStepCount : bestResultList.Count;

				var newResults = GetMinimalMovementPaths(newPiece, endPos, board, newSteps, bestResultStepCount);

				resultList = resultList.AddRange(newResults.Where(x => x.Any()));
			}

			return resultList;
		}
	}
}

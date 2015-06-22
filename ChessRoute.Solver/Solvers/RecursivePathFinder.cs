using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Solvers
{
	public class RecursivePathFinder : IMinimalPathFinder
	{
		public IEnumerable<IList<ChessPiecePosition>> FindMinimalPath(ChessPiece piece, ChessPiecePosition endPos, ChessBoard board)
		{
			if (piece == null) {
				throw new ArgumentNullException("piece");
			}

			if (board == null) {
				throw new ArgumentNullException("board");
			}

			return GetMinimalMovementPaths(piece, endPos, board, ImmutableList<ChessPiecePosition>.Empty, int.MaxValue);
		}

		internal IEnumerable<IList<ChessPiecePosition>> GetMinimalMovementPaths(ChessPiece piece, ChessPiecePosition endPos, ChessBoard board, ImmutableList<ChessPiecePosition> currentSteps, int bestPathStepCount)
		{
			// If we reached the end then return the steps we took to get here
			if (piece.Position == endPos) {
				return ImmutableList<IList<ChessPiecePosition>>.Empty.Add(currentSteps);
			}

			// If the current steps are above the best step count thus far, return
			if (currentSteps.Count >= bestPathStepCount) {
				return ImmutableList<IList<ChessPiecePosition>>.Empty;
			}

			// Get the available step positions on the board and order them by distance
			var availableStepPositions = piece.GetAvailableMovePositions(board)
																.Where(pos => !currentSteps.Contains(pos))
																.OrderBy(pos => pos.DistanceTo(endPos));

			var resultList = ImmutableList<IList<ChessPiecePosition>>.Empty;

			// Iterate over every available step and add the results to the list
			foreach (var availableStepPosition in availableStepPositions) {
				var newPiece = piece.Move(availableStepPosition, board);
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

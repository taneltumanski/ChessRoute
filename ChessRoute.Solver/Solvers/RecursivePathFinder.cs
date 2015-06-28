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
		public IEnumerable<IList<Position>> FindMinimalPath(ChessPiece piece, Position endPos, ChessBoard board)
		{
			if (piece == null) {
				throw new ArgumentNullException("piece");
			}

			if (board == null) {
				throw new ArgumentNullException("board");
			}

			return GetMinimalMovementPaths(piece, endPos, board, ImmutableList<Position>.Empty, int.MaxValue);
		}

		internal IEnumerable<IList<Position>> GetMinimalMovementPaths(ChessPiece piece, Position endPos, ChessBoard board, ImmutableList<Position> currentSteps, int bestPathStepCount)
		{
			// If we reached the end then return the steps we took to get here
			if (piece.Position == endPos) {
				return ImmutableList<IList<Position>>.Empty.Add(currentSteps);
			}

			// If the current steps are above the best step count thus far, return
			if (currentSteps.Count >= bestPathStepCount) {
				return ImmutableList<IList<Position>>.Empty;
			}

			// Get the available step positions on the board and order them by distance
			var availableStepPositions = piece.GetAvailableMovePositions(board)
																.Where(pos => !currentSteps.Contains(pos))
																.OrderBy(pos => pos.DistanceTo(endPos));

			var resultList = ImmutableList<IList<Position>>.Empty;

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

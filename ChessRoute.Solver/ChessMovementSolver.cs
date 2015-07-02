using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using MoreLinq;
using System.Diagnostics;

namespace ChessRoute.Solver
{
	public class ChessMovementSolver : IChessMovementSolver
	{
		private readonly IMinimalPathFinder PathFinder;

		public ChessMovementSolver(IMinimalPathFinder pathFinder)
		{
			if (pathFinder == null) {
				throw new ArgumentNullException("pathFinder");
			}

			this.PathFinder = pathFinder;
		}

		public ChessSolverResult Solve(ChessMovementSolverParameters parameters)
		{
			return Solve(
						new ChessBoard(parameters.BoardWidth, parameters.BoardHeight, parameters.TakenPositions),
						parameters.StartPosition, 
						parameters.EndPosition, 
						parameters.ChessPiece);
		}

		public ChessSolverResult Solve(ChessBoard board, Position startPosition, Position endPosition, ChessPiece chessPiece)
		{
			var stopWatch = Stopwatch.StartNew();

			if (board == null) {
				throw new ArgumentNullException("board");
			}

			if (chessPiece == null) {
				throw new ArgumentNullException("piece");
			}

			if (!board.IsPositionOnBoard(startPosition)) {
				throw new ArgumentException("Start position is not on the board");
			}

			if (!board.IsPositionOnBoard(endPosition)) {
				throw new ArgumentException("End position is not on the board");
			}

			if (!board.IsFreePosition(startPosition)) {
				throw new ArgumentException("Start position is not free");
			}

			if (!board.IsFreePosition(endPosition)) {
				throw new ArgumentException("End position is not free");
			}

			if (startPosition == endPosition) {
				// Create an empty path that indicates a no-step path
				var emptyPath = new List<IList<Position>>() { new List<Position>() };

				return new ChessSolverResult(startPosition, endPosition, chessPiece, board, emptyPath, stopWatch.Elapsed);
			}

			chessPiece = chessPiece.ForceMove(startPosition);

			var minimalPaths = this.PathFinder.FindMinimalPath(chessPiece, endPosition, board);

			minimalPaths = minimalPaths ?? new List<IList<Position>>();

			// Group the results by step count and order it, so the best paths are in the front
			var bestResults = minimalPaths
									.GroupBy(result => result.Count)
									.OrderBy(resultGroup => resultGroup.Key)
									.FirstOrDefault();

			// If the result is not found then create
			var returnResult = bestResults == null ? new List<IList<Position>>() : bestResults.ToList();

			// Remove the first position if it is the start position
			returnResult = returnResult
								.Select(path => path.Any() && path.First() == startPosition ? path.Skip(1).ToList() : path.ToList())
								.Cast<IList<Position>>()
								.ToList();

			stopWatch.Stop();

			return new ChessSolverResult(startPosition, endPosition, chessPiece, board, returnResult, stopWatch.Elapsed);
		}
	}
}

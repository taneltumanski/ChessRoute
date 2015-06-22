using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using MoreLinq;

namespace ChessRoute.Solver.Solvers
{
	public class AStarPathFinder : IMinimalPathFinder
	{
		private readonly bool FindAllPossiblePaths;

		public AStarPathFinder() : this(false) { }
		public AStarPathFinder(bool findAllPossiblePaths)
		{
			this.FindAllPossiblePaths = findAllPossiblePaths;
		}

		public IEnumerable<IList<ChessPiecePosition>> FindMinimalPath(ChessPiece piece, ChessPiecePosition endPosition, ChessBoard board)
		{
			var startPoint = piece.Position;

			var cameFrom = new Dictionary<ChessPiecePosition, ChessPiecePosition>();
			var gScore = new Dictionary<ChessPiecePosition, double>();
			var fScore = new Dictionary<ChessPiecePosition, double>();
			var openSet = new HashSet<ChessPiecePosition>() { startPoint };
			var closedSet = new HashSet<ChessPiecePosition>();

			gScore.Add(startPoint, 0);
			fScore.Add(startPoint, gScore[startPoint] + CostEstimate(startPoint, endPosition));

			closedSet.Add(startPoint);

			while (openSet.Any()) {
				var current = openSet.MinBy(x => fScore[x]);

				if (current == endPosition) {
					var solutionPath = CreatePath(endPosition, cameFrom);

					var solutions = new List<IList<ChessPiecePosition>>();

					solutions.Add(solutionPath.ToList());

					if (this.FindAllPossiblePaths) {
						var allPathsFinder = new RecursivePathFinder();

						var foundPaths =  allPathsFinder.GetMinimalMovementPaths(piece, endPosition, board, ImmutableList<ChessPiecePosition>.Empty, solutionPath.Count());

						foreach (var path in foundPaths) {
							solutions.Add(path);
						}
					}

					return solutions;
				}

				openSet.Remove(current);
				closedSet.Add(current);

				var currentPositionPiece = piece.ForceMove(current);

				var availableMovePositions = currentPositionPiece.GetAvailableMovePositions(board)
																					.Where(pos => !closedSet.Contains(pos))
																					.OrderBy(pos => pos.DistanceTo(endPosition));

				foreach (var neighbor in availableMovePositions) {
					double tentativeGScore = gScore[current] + CostEstimate(current, neighbor);

					if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor]) {
						cameFrom[neighbor] = current;
						gScore[neighbor] = tentativeGScore;
						fScore[neighbor] = gScore[neighbor] + CostEstimate(neighbor, endPosition);

						openSet.Add(neighbor);
					}
				}
			}

			return new List<IList<ChessPiecePosition>>();
		}

		private IEnumerable<ChessPiecePosition> CreatePath(ChessPiecePosition current, Dictionary<ChessPiecePosition, ChessPiecePosition> cameFrom)
		{
			var list = new List<ChessPiecePosition>();

			while (true) {
				list.Add(current);

				if (!cameFrom.ContainsKey(current)) {
					break;
				} else {
					current = cameFrom[current];
				}
			}

			// Reverse the list becase we traverse the step tree backwards
			return new ReadOnlyCollection<ChessPiecePosition>(Enumerable.Reverse(list).ToList());
		}

		private double CostEstimate(ChessPiecePosition a, ChessPiecePosition b)
		{
			return a.DistanceTo(b);
		}
	}
}

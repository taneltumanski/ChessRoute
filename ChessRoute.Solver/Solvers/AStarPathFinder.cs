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

		public IEnumerable<IList<Position>> FindMinimalPath(ChessPiece piece, Position endPosition, ChessBoard board)
		{
			var startPoint = piece.Position;

			var cameFrom = new Dictionary<Position, Position>();
			var gScore = new Dictionary<Position, double>();
			var fScore = new Dictionary<Position, double>();
			var distScore = new Dictionary<Position, double>();
			var openSet = new HashSet<Position>() { startPoint };
			var closedSet = new HashSet<Position>();

			gScore[startPoint] = 0;
			fScore[startPoint] = gScore[startPoint] + 1;
			distScore[startPoint] = 0;

			while (openSet.Any()) {
				var current = openSet
								.GroupBy(x => fScore[x])
								.MinBy(x => x.Key)
								.OrderBy(x => distScore[x])
								.First();

				if (current == endPosition) {
					var solutionPath = CreatePath(endPosition, cameFrom);

					var solutions = new List<IList<Position>>();

					solutions.Add(solutionPath.ToList());

					if (this.FindAllPossiblePaths) {
						var allPathsFinder = new RecursivePathFinder();

						var foundPaths = allPathsFinder.GetMinimalMovementPaths(
																	piece,
																	endPosition,
																	board,
																	ImmutableList<Position>.Empty,
																	solutionPath.Count());

						foreach (var path in foundPaths) {
							solutions.Add(path);
						}
					}

					return solutions;
				}

				openSet.Remove(current);
				closedSet.Add(current);

				var currentPositionPiece = piece.ForceMove(current);

				var availableMovePositions = currentPositionPiece.GetAvailableMovePositions(board).Where(pos => !closedSet.Contains(pos));

				foreach (var neighbor in availableMovePositions) {
					double tentativeGScore = gScore[current] + 1;

					if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor]) {
						cameFrom[neighbor] = current;
						gScore[neighbor] = tentativeGScore;
						fScore[neighbor] = gScore[neighbor] + 1;
						distScore[neighbor] = distScore[current] + current.DistanceTo(neighbor);

						openSet.Add(neighbor);
					}
				}
			}

			return new List<IList<Position>>();
		}

		private IEnumerable<Position> CreatePath(Position current, Dictionary<Position, Position> cameFrom)
		{
			var list = new List<Position>();

			while (true) {
				list.Add(current);

				if (!cameFrom.ContainsKey(current)) {
					break;
				} else {
					current = cameFrom[current];
				}
			}

			// Reverse the list becase we traverse the step tree backwards
			return new ReadOnlyCollection<Position>(Enumerable.Reverse(list).ToList());
		}
	}
}

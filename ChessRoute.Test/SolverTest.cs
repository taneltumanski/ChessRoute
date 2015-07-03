using System;
using ChessRoute.Solver;
using ChessRoute.Solver.Solvers;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ChessRoute.Test
{
	[TestClass]
	public class SolverTest
	{
		private IChessMovementSolver Solver;

		[TestInitialize]
		public void Init()
		{
			Solver = new ChessMovementSolver(new AStarPathFinder());
		}

		[TestMethod]
		public void RookGetsOptimalPath()
		{
			int optimalPathCount = 2;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("A1"),
													Position.FromString("H8"),
													null,
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.Rook),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void QueenGetsOptimalPath()
		{
			int optimalPathCount = 2;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("F6"),
													Position.FromString("H8"),
													new List<Position>() { Position.FromString("G7") },
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.Queen),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void QueenGetsOptimalPath2()
		{
			int optimalPathCount = 1;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("A1"),
													Position.FromString("H8"),
													new List<Position>(),
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.Queen),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void KingGetsOptimalPath()
		{
			int optimalPathCount = 3;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("F6"),
													Position.FromString("H8"),
													new List<Position>() { Position.FromString("G7") },
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.King),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void KingGetsOptimalPath2()
		{
			int optimalPathCount = 7;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("A1"),
													Position.FromString("H8"),
													new List<Position>(),
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.King),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void BishopGetsOptimalPath()
		{
			int optimalPathCount = 1;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("A1"),
													Position.FromString("H8"),
													new List<Position>(),
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.Bishop),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void BishopGetsOptimalPath2()
		{
			int optimalPathCount = 2;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("C1"),
													Position.FromString("H8"),
													new List<Position>(),
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.Bishop),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}

		[TestMethod]
		public void BishopGetsOptimalPath3()
		{
			int optimalPathCount = 2;
			var parameters = new ChessMovementSolverParameters(
													Position.FromString("E1"),
													Position.FromString("H8"),
													new List<Position>(),
													new ChessPieceFactory().CreateChessPiece(ChessPieceOption.Bishop),
													8,
													8);

			var result = Solver.Solve(parameters);

			Assert.IsTrue(result.HasSolution, "No solution");
			Assert.IsTrue(result.MinimalPaths.First().Count == optimalPathCount, string.Format("Step count wrong - was {0}, should be {1}", result.MinimalPaths.First().Count(), optimalPathCount));
		}
	}
}

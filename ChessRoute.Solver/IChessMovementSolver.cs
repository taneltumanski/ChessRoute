using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public interface IChessMovementSolver
	{
		ChessSolverResult Solve(ChessMovementSolverParameters parameters);
		ChessSolverResult Solve(ChessBoard board, Position startPosition, Position endPosition, ChessPiece piece);
	}
}

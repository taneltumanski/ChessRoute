using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public class DefaultChessMovementSolverParameters : ChessMovementSolverParameters
	{
		public DefaultChessMovementSolverParameters(ChessPiecePosition startPos, ChessPiecePosition endPos, IEnumerable<ChessPiecePosition> takenPositions) 
			: base(startPos, endPos, takenPositions, new Knight(), ChessBoard.DEFAULT_BOARD_WIDTH, ChessBoard.DEFAULT_BOARD_HEIGHT) { }
	}
}

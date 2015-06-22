using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public interface IMinimalPathFinder
	{
		IEnumerable<IList<ChessPiecePosition>> FindMinimalPath(ChessPiece piece, ChessPiecePosition endPosition, ChessBoard board);
	}
}

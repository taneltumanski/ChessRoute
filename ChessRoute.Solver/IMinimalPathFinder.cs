using ChessRoute.Solver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public interface IMinimalPathFinder
	{
		IEnumerable<IList<Position>> FindMinimalPath(ChessPiece piece, Position endPosition, ChessBoard board);
	}
}

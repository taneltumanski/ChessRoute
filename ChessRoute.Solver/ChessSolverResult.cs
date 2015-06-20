using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
	public class ChessSolverResult
	{
		public IEnumerable<IList<ChessPiecePosition>> MinimalPaths { get; private set; }

		public ChessSolverResult(IEnumerable<IList<ChessPiecePosition>> minimalPaths)
		{
			if (minimalPaths == null) {
				throw new ArgumentNullException("minimalPaths");
			}

			this.MinimalPaths = new ReadOnlyCollection<IList<ChessPiecePosition>>(minimalPaths.ToList());
		}
	}
}

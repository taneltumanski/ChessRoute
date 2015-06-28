using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Input
{
	public class InputParameters
	{
		public string StartPosition { get; set; }
		public string EndPosition { get; set; }
		public ChessPieceOption ChessPiece { get; set; }
		public IEnumerable<string> TakenPositions { get; set; }
		public int BoardWidth { get; set; }
		public int BoardHeight { get; set; }

		public ChessMovementSolverParameters ToSolverParameters()
		{
			var startPos = Position.FromString(this.StartPosition);
			var endPos = Position.FromString(this.EndPosition);
			var chessPiece = new ChessPieceFactory().CreateChessPiece(this.ChessPiece);
			var takenPositions = this.TakenPositions.Select(posString => Position.FromString(posString));

			return new ChessMovementSolverParameters(startPos, endPos, takenPositions, chessPiece, this.BoardWidth, this.BoardHeight);
		}
	}
}

using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChessRoute.Web.Models
{
	public class ChessParameterModel
	{
		[Required]
		[Display(Name = "Start position")]
		public string StartPosition { get; set; }

		[Required]
		[Display(Name = "End position")]
		public string EndPosition { get; set; }

		[Required]
		[Display(Name = "Chess piece")]
		public ChessPieceOption ChessPiece { get; set; }

		[Required]
		public IEnumerable<string> TakenPositions { get; set; }

		[Required]
		[Display(Name = "Board width")]
		public int BoardWidth { get; set; }

		[Required]
		[Display(Name = "Board height")]
		public int BoardHeight { get; set; }
	}
}
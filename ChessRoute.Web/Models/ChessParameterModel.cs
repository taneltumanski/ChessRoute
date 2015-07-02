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
		public static readonly ChessParameterModel Default = new ChessParameterModel() {
			StartPosition = "A1",
			EndPosition = "H8",
			ChessPiece = ChessPieceOption.Knight,
			BoardHeight = 8,
			BoardWidth = 8,
			TakenPositionsList = new string[] { "B2", "D7", "G6" }
		};

		[Required]
		[Display(Name = "Start position")]
		[RegularExpression("[a-zA-Z]+\\d+")]
		public string StartPosition { get; set; }

		[Required]
		[Display(Name = "End position")]
		[RegularExpression("[a-zA-Z]+\\d+")]
		public string EndPosition { get; set; }

		[Required]
		[Display(Name = "Chess piece")]
		[EnumDataType(typeof(ChessPieceOption))]
		public ChessPieceOption ChessPiece { get; set; }

		[Display(Name = "Taken positions")]
		[DataType(DataType.Text)]
		public string TakenPositions { get; set; }

		[Required]
		[Display(Name = "Board width")]
		[Range(1, int.MaxValue)]
		public int BoardWidth { get; set; }

		[Required]
		[Display(Name = "Board height")]
		[Range(1, int.MaxValue)]
		public int BoardHeight { get; set; }

		public IEnumerable<string> TakenPositionsList { 
			get { 
				return TakenPositions == null ? new string[0] : TakenPositions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()); 
			}

			set {
				TakenPositions = string.Join(", ", value);
			}
		}
	}
}
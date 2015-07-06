using ChessRoute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChessRoute.Solver;

namespace ChessRoute.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(ChessParameterModel model = null)
		{
			if (model == null || !ModelState.IsValid) {
				model = ChessParameterModel.Default;
			}
			
			return View(model);
		}

		public ActionResult Share(
			string start = null, 
			string end = null,
			int width = 0,
			int height = 0,
			string takenPositions = null,
			string chessPiece = null)
		{
			var piece = ChessPieceOption.Knight;

			if (chessPiece != null) {
				if (!Enum.TryParse(chessPiece, true, out piece)) {
					piece = ChessPieceOption.Knight;
				}
			}

			var model = new ChessParameterModel() {
				StartPosition = start ?? ChessParameterModel.Default.StartPosition,
				EndPosition = end ?? ChessParameterModel.Default.EndPosition,
				BoardWidth = width > 0 ? width : ChessParameterModel.Default.BoardWidth,
				BoardHeight = height > 0 ? height : ChessParameterModel.Default.BoardHeight,
				TakenPositions = takenPositions ?? ChessParameterModel.Default.TakenPositions,
				ChessPiece = piece
			};

			return View("Index", model);
		}
    }
}

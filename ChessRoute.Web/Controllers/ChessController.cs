using ChessRoute.Solver;
using ChessRoute.Solver.Solvers;
using ChessRoute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChessRoute.Web.Controllers
{
    public class ChessController : Controller
    {
        public JsonResult SolveRoute(ChessParameterModel model)
        {
			if (model == null) {
				throw new ArgumentNullException("model");
			}

			var startPos = Position.FromString(model.StartPosition);
			var endPos = Position.FromString(model.EndPosition);
			var chessPiece = new ChessPieceFactory().CreateChessPiece(model.ChessPiece);
			var takenPositions = model.TakenPositions.Select(posString => Position.FromString(posString));

			var parameters = new ChessMovementSolverParameters(startPos, endPos, takenPositions, chessPiece, model.BoardWidth, model.BoardHeight);
			var solver = new ChessMovementSolver(new AStarPathFinder());

			var result = solver.Solve(parameters);

			var firstMinPath = result.MinimalPaths.FirstOrDefault();
			var jsonResult = new SolverResult() { HasPath = firstMinPath != null, Path = firstMinPath.Select(pos => pos.ToString()) };

			return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

		public class SolverResult
		{
			public bool HasPath { get; set; }
			public IEnumerable<string> Path { get; set; }
		}
    }
}

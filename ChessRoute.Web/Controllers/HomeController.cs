using ChessRoute.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
	}
}

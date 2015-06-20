﻿using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute
{
	public interface IResultWriter
	{
		void Write(ChessSolverResult result);
	}
}

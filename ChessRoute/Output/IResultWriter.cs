﻿using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Output
{
	public interface IResultWriter
	{
		void Write(ChessSolverResult result);
	}
}

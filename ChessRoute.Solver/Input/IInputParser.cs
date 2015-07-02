using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver.Input
{
	public interface IInputParser<T> where T : class
	{
		InputParameters Parse(T data);
	}
}

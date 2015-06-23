using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Input
{
	public interface IInputParser
	{
		InputParameters Parse(string data);
	}
}

using ChessRoute.Input;
using ChessRoute.Solver;
using ChessRoute.Solver.Solvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ChessRoute
{
	public class Program
	{
		public static int Main(string[] args)
		{
			if (args.Length < 2) {
				using (var process = Process.GetCurrentProcess()) {
					Console.WriteLine("Usage:");
					Console.WriteLine(string.Format("\t{0} {input file} {output file}", Path.GetFileName(process.MainModule.FileName)));
				}

				return 1;
			}

			var inputFile = args[0];
			var outputFile = args[1];

			var parser = new FileParser(new List<IInputParser>() { new OriginalInputParser(), new JSONInputParser() });

			var inputParameters = parser.Parse(inputFile);
			var chessSolverParameters = inputParameters.ToSolverParameters();
			var solver = new ChessMovementSolver(new AStarPathFinder());
			var result = solver.Solve(chessSolverParameters);

			var resultOutputWriter = new FileResultWriter(outputFile);

			resultOutputWriter.Write(result);

			return 0;
		}
	}
}

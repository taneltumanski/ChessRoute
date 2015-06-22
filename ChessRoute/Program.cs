using ChessRoute.Solver;
using ChessRoute.Solver.Solvers;
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

			var parser = new FileParser();

			var inputParameters = parser.Parse(inputFile);
			var chessSolverParameters = inputParameters.ToSolverParameters();
			var solver = new ChessMovementSolver(new AStarPathFinder());
			var result = solver.Solve(chessSolverParameters);

			var resultOutputWriter = new FileResultWriter(outputFile);

			resultOutputWriter.Write(result);

			Console.WriteLine("Time = " + result.TimeTaken);
			Console.WriteLine("Piece = " + result.ChessPiece.GetType().Name);
			Console.WriteLine("Board = " + result.ChessBoard.Width + "x" + result.ChessBoard.Height);
			Console.WriteLine("Start pos = " + result.StartPosition);
			Console.WriteLine("End pos = " + result.EndPosition);
			Console.WriteLine("Path found = " + result.HasSolution);

			if (result.HasSolution) {
				Console.WriteLine("Solution count = " + result.MinimalPaths.Count());
				Console.WriteLine("Step count = " + result.MinimalPaths.First().Count);
			}

			return 0;
		}
	}
}

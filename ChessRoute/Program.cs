using ChessRoute.Solver;
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

			var parserFactory = new FileParserFactory();
			var parser = parserFactory.CreateFileParser(inputFile);

			var parameters = parser.Parse(inputFile);
			var solver = new ChessMovementSolver(parameters);
			var result = solver.Solve();

			var resultOutputWriter = new FileResultWriter(outputFile);

			resultOutputWriter.Write(result);

			return 0;
		}
	}
}

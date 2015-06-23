using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChessRoute.Output
{
	public class FileResultWriter : IResultWriter
	{
		private readonly string _filePath;

		public FileResultWriter(string path)
		{
			if (string.IsNullOrWhiteSpace(path)) {
				throw new ArgumentNullException("path");
			}

			this._filePath = path;
		}

		public void Write(ChessSolverResult result)
		{
			var sb = new StringBuilder();

			foreach (var resultPath in result.MinimalPaths) {
				sb.AppendLine(resultPath.Count.ToString());
				sb.AppendLine(string.Join(", ", resultPath.Select(pos => pos.ToString())));
			}

			File.WriteAllText(this._filePath, sb.ToString());
		}
	}
}

using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChessRoute
{
	public class DefaultFileParser : IFileParser
	{
		public ChessMovementSolverParameters Parse(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath)) {
				throw new ArgumentNullException("filePath");
			}

			if (!File.Exists(filePath)) {
				throw new ArgumentException("File does not exist - " + filePath);
			}

			var lines = File.ReadAllLines(filePath);

			if (lines.Length != 3) {
				throw new ArgumentException("Config file is incorrect");
			}

			var startPosString = lines[0].Trim();
			var endPosString = lines[1].Trim();
			var takenPositionsStrings = lines[2].Trim().Split(',').Select(x => x.Trim()).ToList();

			var startPos = ChessPiecePosition.FromString(startPosString);
			var endPos = ChessPiecePosition.FromString(endPosString);
			var takenPositions = takenPositionsStrings.Select(posString => ChessPiecePosition.FromString(posString));

			return new DefaultChessMovementSolverParameters(startPos, endPos, takenPositions);
		}
	}
}

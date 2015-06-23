﻿using ChessRoute.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessRoute.Input
{
	public class OriginalInputParser : IInputParser
	{
		public InputParameters Parse(string data)
		{
			var lines = data.Split(new [] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();

			var startPosString = lines[0];
			var endPosString = lines[1];
			var takenPositionsStrings = lines[2].Split(',').Select(x => x.Trim()).ToList();
			var chessPiece = ChessPieceOption.Knight;
			var boardWidth = 8;
			var boardHeight = 8;

			if (lines.Length > 3) {
				chessPiece = (ChessPieceOption)Enum.Parse(typeof(ChessPieceOption), lines[3], true);
			}

			if (lines.Length > 4) {
				boardWidth = int.Parse(lines[4]);
			}

			if (lines.Length > 5) {
				boardHeight = int.Parse(lines[5]);
			}

			return new InputParameters() {
				StartPosition = startPosString,
				EndPosition = endPosString,
				TakenPositions = takenPositionsStrings,
				ChessPiece = chessPiece,
				BoardWidth = boardWidth,
				BoardHeight = boardHeight,
			};
		}
	}
}

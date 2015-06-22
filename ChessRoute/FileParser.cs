using ChessRoute.Solver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChessRoute
{
	public class FileParser
	{
		public InputFileParameters Parse(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath)) {
				throw new ArgumentNullException("filePath");
			}

			if (!File.Exists(filePath)) {
				throw new ArgumentException("File does not exist - " + filePath);
			}

			var lines = File.ReadAllLines(filePath).Select(x => x.Trim()).ToArray();

			InputFileParameters parameters = null;

			try {
				parameters = ParseDefault(lines);
			} catch (Exception) { }

			if (parameters == null) {
				var joinedString = string.Join("", lines);

				try {
					parameters = JsonConvert.DeserializeObject<InputFileParameters>(joinedString, new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Error });
				} catch (Exception) { }
			}

			if (parameters == null) {
				throw new ArgumentException("Could not parse input file");
			}

			return parameters;
		}

		private InputFileParameters ParseDefault(string[] lines)
		{
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

			return new InputFileParameters() {
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

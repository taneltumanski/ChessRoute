using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChessRoute.Solver
{
	public struct ChessPiecePosition
	{
		public int Row { get; private set; }
		public int Column { get; private set; }

		public ChessPiecePosition(int row, int col) : this()
		{
			this.Row = row;
			this.Column = col;
		}

		public double DistanceTo(ChessPiecePosition pos)
		{
			var a = this.Row - pos.Row;
			var b = this.Column - pos.Column;

			return Math.Sqrt(a * a + b * b);
		}

		public override bool Equals(object obj)
		{
			if (obj is ChessPiecePosition) {
				var pos = (ChessPiecePosition)obj;

				return pos.Row == this.Row && pos.Column == this.Column;
			}

			return false;
		}

		public static bool operator ==(ChessPiecePosition first, ChessPiecePosition second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(ChessPiecePosition first, ChessPiecePosition second)
		{
			return !first.Equals(second);
		}

		public override int GetHashCode()
		{
			return this.Row + 13 * this.Column;
		}

		public override string ToString()
		{
			return string.Format("{0}{1}", RowToCharacters(this.Row), this.Column + 1);
		}

		public static ChessPiecePosition FromString(string positionInput)
		{
			// Check if the input string is in the form of A1, AA11, AZZASD112312 etc
			if (!Regex.IsMatch(positionInput, @"[a-zA-Z]+\d+")) {
				throw new ArgumentException("Position input invalid");
			}

			positionInput = positionInput.ToUpperInvariant();

			var characters = new string(positionInput.TakeWhile(c => char.IsLetter(c)).ToArray());
			var numbers = new string(positionInput.SkipWhile(c => char.IsLetter(c)).ToArray());

			var row = CharactersToRow(characters);
			var column = int.Parse(numbers) - 1;

			return new ChessPiecePosition(row, column);
		}

		private static int CharactersToRow(string characters)
		{
			int sum = 0;

			for (int i = 0; i < characters.Length; i++) {
				var c = characters[i];

				var characterIndex = char.ToUpperInvariant(c) - 'A';

				sum += (i + 1) * characterIndex;
			}

			return sum;
		}

		private static string RowToCharacters(int row)
		{
			var value = row;

			var sb = new StringBuilder();

			//TODO CORRECT METHOD PLZ
			sb.Append((char)('A' + row));

			return sb.ToString();
		}
	}
}

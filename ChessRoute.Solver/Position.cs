using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ChessRoute.Solver
{
	public struct Position
	{
		public int Row { get; private set; }
		public int Column { get; private set; }

		public Position(int row, int col) : this()
		{
			this.Row = row;
			this.Column = col;
		}

		public double DistanceTo(Position pos)
		{
			var a = this.Row - pos.Row;
			var b = this.Column - pos.Column;

			return Math.Sqrt(a * a + b * b);
		}

		public override bool Equals(object obj)
		{
			if (obj is Position) {
				var pos = (Position)obj;

				return pos.Row == this.Row && pos.Column == this.Column;
			}

			return false;
		}

		public static bool operator ==(Position first, Position second)
		{
			return first.Equals(second);
		}

		public static bool operator !=(Position first, Position second)
		{
			return !first.Equals(second);
		}

		public override int GetHashCode()
		{
			return this.Row + 13 * this.Column;
		}

		public override string ToString()
		{
			return string.Format("{0}{1}", ColumnToCharacters(this.Column), this.Row + 1);
		}

		public static Position FromString(string positionInput)
		{
			// Check if the input string is in the form of A1, AA11, AZZASD112312 etc
			if (!Regex.IsMatch(positionInput, @"[a-zA-Z]+\d+")) {
				throw new ArgumentException("Position input invalid");
			}

			positionInput = positionInput.ToUpperInvariant();

			var characters = new string(positionInput.TakeWhile(c => char.IsLetter(c)).ToArray());
			var numbers = new string(positionInput.SkipWhile(c => char.IsLetter(c)).ToArray());

			var row = int.Parse(numbers) - 1;
			var column = CharactersToColumn(characters);

			return new Position(row, column);
		}

		private static int CharactersToColumn(string characters)
		{
			int sum = 0;
			int power = 1;

			for (int i = 0; i < characters.Length; i++) {
				var c = characters[i];

				var charValue = char.ToUpperInvariant(c) - 'A' + 1;

				sum += power * charValue;
				power *= 26;
			}

			return sum - 1;
		}

		private static string ColumnToCharacters(int column)
		{
			var value = column + 1;

			var sb = new StringBuilder();

			while (value > 0) {
				var charIndex = value % 26;
				var character = (char)('A' + charIndex - 1);

				sb.Append(character);

				value -= charIndex;
				value /= 26;
			}

			return sb.ToString();
		}
	}
}

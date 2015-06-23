using ChessRoute.Input;
using ChessRoute.Solver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ChessRoute
{
	public class FileParser
	{
		private readonly ReadOnlyCollection<IInputParser> Parsers;

		public FileParser() : this(new List<IInputParser>() { new OriginalInputParser(), new JSONInputParser() }) { }
		public FileParser(IEnumerable<IInputParser> parsers)
		{
			if (parsers == null) {
				throw new ArgumentNullException("parsers");
			}

			var parserList = parsers.ToList();

			if (!parserList.Any()) {
				throw new ArgumentException("No parsers defined");
			}

			this.Parsers = new ReadOnlyCollection<IInputParser>(parserList);
		}

		public InputParameters Parse(string filePath)
		{
			if (string.IsNullOrWhiteSpace(filePath)) {
				throw new ArgumentNullException("filePath");
			}

			if (!File.Exists(filePath)) {
				throw new ArgumentException("File does not exist - " + filePath);
			}

			var text = File.ReadAllText(filePath);

			if (string.IsNullOrWhiteSpace(text)) {
				throw new ArgumentException("File data is invalid");
			}

			foreach (var parser in this.Parsers) {
				try {
					return parser.Parse(text);
				} catch (Exception) { }
			}

			throw new ArgumentException("Could not parse input file");
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChessRoute
{
	public class FileParserFactory
	{
		public IFileParser CreateFileParser(string inputFile)
		{
			if (string.IsNullOrWhiteSpace(inputFile)) {
				throw new ArgumentNullException("filePath");
			}

			if (!File.Exists(inputFile)) {
				throw new ArgumentException("File does not exist - " + inputFile);
			}

			var lines = File.ReadAllLines(inputFile);

			if (lines.Length == 3) {
				return new DefaultFileParser();
			}

			throw new InvalidOperationException("No parser could be determined");
		}
	}
}

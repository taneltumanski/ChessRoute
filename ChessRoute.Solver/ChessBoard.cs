using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
    public class ChessBoard
    {
		private static readonly int DEFAULT_BOARD_WIDTH = 8;
		private static readonly int DEFAULT_BOARD_HEIGHT = 8;

		private readonly bool[][] _positionData;

		private readonly int _width;
		private readonly int _height;

		public int Width { get { return _width; } }
		public int Height { get { return _height; } }

		private readonly ReadOnlyCollection<ChessPiecePosition> _takenPositions;
		public IEnumerable<ChessPiecePosition> TakenPositions { get { return _takenPositions; } }

		public ChessBoard(IEnumerable<ChessPiecePosition> takenPositions) : this(DEFAULT_BOARD_WIDTH, DEFAULT_BOARD_HEIGHT, takenPositions) { }
		public ChessBoard(int width, int height, IEnumerable<ChessPiecePosition> takenPositions)
		{
			if (width <= 0) {
				throw new ArgumentException("Board width cannot be <= 0");
			}

			if (height <= 0) {
				throw new ArgumentException("Board height cannot be <= 0");
			}

			if (takenPositions == null) {
				throw new ArgumentNullException("takenPositions");
			}

			var takenPositionsList = takenPositions.ToImmutableList();
			
			this._width = width;
			this._height = height;
			this._positionData = GetAvailablePositions(width, height, takenPositionsList);
			this._takenPositions = new ReadOnlyCollection<ChessPiecePosition>(takenPositionsList);
		}

		private bool[][] GetAvailablePositions(int width, int height, IEnumerable<ChessPiecePosition> takenPositions)
		{
			var rowBuilder = new bool[height][];

			for (int row = 0; row < rowBuilder.Length; row++) {
				var columnBuilder = new bool[width];

				for (int column = 0; column < columnBuilder.Length; column++) {
					bool isFreePosition = true;

					if (takenPositions.Any(pos => pos.Column == column && pos.Row == row)) {
						isFreePosition = false;
					}

					columnBuilder[column] = isFreePosition;
				}

				rowBuilder[row] = columnBuilder;
			}

			return rowBuilder;
		}

		public bool IsFreePosition(ChessPiecePosition pos)
		{
			if (!IsPositionOnBoard(pos)) {
				throw new ArgumentException("New position is not on the board");
			}

			return _positionData[pos.Row][pos.Column];
		}

		public bool IsPositionOnBoard(ChessPiecePosition pos)
		{
			if (pos.Row < 0 || pos.Column < 0) {
				return false;
			}

			return _positionData.Length > pos.Row && _positionData[pos.Row].Length > pos.Column;
		}
    }
}

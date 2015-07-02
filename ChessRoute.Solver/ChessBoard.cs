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

		private readonly bool[,] _positionData;

		private readonly int _width;
		private readonly int _height;

		public int Width { get { return _width; } }
		public int Height { get { return _height; } }

		private readonly ReadOnlyCollection<Position> _takenPositions;
		public IEnumerable<Position> TakenPositions { get { return _takenPositions; } }

		public ChessBoard(IEnumerable<Position> takenPositions) : this(DEFAULT_BOARD_WIDTH, DEFAULT_BOARD_HEIGHT, takenPositions) { }
		public ChessBoard(int width, int height, IEnumerable<Position> takenPositions)
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
			this._takenPositions = new ReadOnlyCollection<Position>(takenPositionsList);
		}

		private bool[,] GetAvailablePositions(int width, int height, IEnumerable<Position> takenPositions)
		{
			var array = new bool[width, height];

			for (int row = 0; row < height; row++) {
				for (int column = 0; column < width; column++) {
					bool isFreePosition = true;

					if (takenPositions.Any(pos => pos.Column == column && pos.Row == row)) {
						isFreePosition = false;
					}

					array[column, row] = isFreePosition;
				}
			}

			return array;
		}

		public bool IsFreePosition(Position pos)
		{
			if (!IsPositionOnBoard(pos)) {
				throw new ArgumentException("New position is not on the board");
			}

			return _positionData[pos.Column, pos.Row];
		}

		public bool IsPositionOnBoard(Position pos)
		{
			if (pos.Row < 0 || pos.Column < 0) {
				return false;
			}

			return _positionData.GetLength(0) > pos.Column && _positionData.GetLength(1) > pos.Row;
		}
    }
}

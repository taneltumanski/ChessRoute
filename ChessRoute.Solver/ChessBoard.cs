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

		private readonly ImmutableDictionary<int, ImmutableDictionary<int, bool>> _positionData;

		private readonly int _width;
		private readonly int _height;

		public int Width { get { return _width; } }
		public int Height { get { return _height; } }

		public ChessBoard(IEnumerable<ChessPiecePosition> takenPositions) : this(DEFAULT_BOARD_WIDTH, DEFAULT_BOARD_HEIGHT, takenPositions) { }
		public ChessBoard(int width, int height, IEnumerable<ChessPiecePosition> takenPositions)
		{
			if (width < 0) {
				throw new ArgumentException("Board width cannot be < 0");
			}

			if (height < 0) {
				throw new ArgumentException("Board height cannot be < 0");
			}

			if (takenPositions == null) {
				throw new ArgumentNullException("takenPositions");
			}

			this._width = width;
			this._height = height;
			this._positionData = GetAvailablePositions(width, height, takenPositions.ToImmutableList());
		}

		private ImmutableDictionary<int, ImmutableDictionary<int, bool>> GetAvailablePositions(int width, int height, IEnumerable<ChessPiecePosition> takenPositions)
		{
			var rowBuilder = ImmutableDictionary.CreateBuilder<int, ImmutableDictionary<int, bool>>();

			for (int row = 0; row < height; row++) {
				var columnBuilder = ImmutableDictionary.CreateBuilder<int, bool>();

				for (int column = 0; column < width; column++) {
					bool isFreePosition = true;

					if (takenPositions.Any(pos => pos.Column == column && pos.Row == row)) {
						isFreePosition = false;
					}

					columnBuilder[column] = isFreePosition;
				}

				rowBuilder[row] = columnBuilder.ToImmutable();
			}

			return rowBuilder.ToImmutable();
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
			return _positionData.ContainsKey(pos.Row) && _positionData[pos.Row].ContainsKey(pos.Column);
		}
    }
}

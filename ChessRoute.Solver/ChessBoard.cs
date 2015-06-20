﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChessRoute.Solver
{
    public class ChessBoard
    {
		internal static readonly int DEFAULT_BOARD_WIDTH = 8;
		internal static readonly int DEFAULT_BOARD_HEIGHT = 8;

		private readonly ImmutableDictionary<int, ImmutableDictionary<int, bool>> _positionData;

		public IEnumerable<ChessPiecePosition> FreePositions {
			get {
				foreach (var row in _positionData) {
					foreach (var column in _positionData[row.Key]) {
						if (_positionData[row.Key][column.Key]) {
							yield return new ChessPiecePosition(row.Key, column.Key);
						}
					}
				}
			}
		}

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
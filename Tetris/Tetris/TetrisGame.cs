﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;

namespace Tetris
{
	class TetrisGame
	{
		// Classic tetris appears to be 10 square wide, and 18 square high.

		// Pixels to manipulate
		WriteableBitmap writeableBitmap;
		bool isGameOver;
		//int score;
		public Square theSquare;
		I_Shape currentShape;
		Square[,] landedSquares;
		public static int unitLength;
		static int left = 100;
		int right;
		static int top = 100;
		int bottom;



		public TetrisGame(WriteableBitmap wb, int unitLen)
		{
			writeableBitmap = wb;
			isGameOver = false;
			unitLength = unitLen;
			right = (left + (10 * unitLength));
			bottom = (top + (18 * unitLength));
			landedSquares = new Square[10, 18];
		}

		public void init()
		{
			currentShape = new I_Shape(left + 3 * unitLength, top, unitLength);
		}

		public void render()
		{
			writeableBitmap.Clear();
			writeableBitmap.DrawRectangle(left, top, right, bottom, Colors.Black);
			currentShape.render(writeableBitmap);
			renderLandedSquares();
		}

		private void renderLandedSquares()
		{
			foreach (var square in landedSquares)
			{
				if (square != null)
				{
					square.render(writeableBitmap);
				}
			}
		}

		internal void changeState()
		{
			if (isSpaceForShapeToDrop())
			{
				currentShape.moveDown();
			}
			else
			{
				addShapeToLandedSquares();
				currentShape = new I_Shape(left + 3 * unitLength, top, unitLength);
			}
		}

		private void addShapeToLandedSquares()
		{
			foreach (var square in currentShape.squares)
			{
				var xIndex = (square.x - left) / unitLength;
				var yIndex = (square.y - top) / unitLength;
				landedSquares[xIndex, yIndex] = square;
			}
		}

		private bool isSpaceForShapeToDrop()
		{
			foreach (var square in currentShape.squares)
			{
				if (!isSpaceToDrop(square))
				{
					return false;
				}
			}
			return true;
		}



		private bool isSpaceToDrop(Square square)
		{
			var isAboveBottom = square.y + unitLength < bottom;
			return isAboveBottom && isAboveAllLanded(square);
		}

		private bool isAboveAllLanded(Square square)
		{
			foreach (var sq in landedSquares)
			{
				if (sq != null && sq.x == square.x && sq.y == square.y + unitLength)
				{
					return false;
				}
			}
			return true;
		}

		internal void moveLeft()
		{
			if (isLeftFree())
			{
				theSquare.x -= unitLength;
			}
		}

		private bool isLeftFree()
		{
			if (theSquare.x > left)
			{
				foreach (var square in landedSquares)
				{
					if (square != null && square.x == theSquare.x - unitLength && square.y == theSquare.y)
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		internal void moveRight()
		{
			if (isRightFree())
			{
				theSquare.x += unitLength;
			}
		}

		private bool isRightFree()
		{
			if (theSquare.x + unitLength < right)
			{
				foreach (var square in landedSquares)
				{
					if (square != null && square.x == theSquare.x + unitLength && square.y == theSquare.y)
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}

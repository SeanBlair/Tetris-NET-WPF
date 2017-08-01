using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;
using Tetris.Shapes;

namespace Tetris
{
	class TetrisGame
	{
		// Classic tetris appears to be 10 square wide, and 18 square high.

		// Pixels to manipulate
		WriteableBitmap writeableBitmap;
		Shape currentShape;
		Square[,] landedSquares;
		public static int unitLength;
		static int left = 100;
		int right;
		static int top = 100;
		int bottom;
		Random random;


		public TetrisGame(WriteableBitmap wb, int unitLen)
		{
			writeableBitmap = wb;
			unitLength = unitLen;
			right = (left + (10 * unitLength));
			bottom = (top + (18 * unitLength));
			landedSquares = new Square[10, 18];
			random = new Random();
		}

		public void init()
		{
			currentShape = getNextShape();
		}

		private Shape getNextShape()
		{
			Shape next = null;
			switch (random.Next(2))
			{
				case 0:
					next = new S_Shape(left + 3 * unitLength, top, unitLength);
					break;
				case 1:
					next = new T_Shape(left + 3 * unitLength, top, unitLength);
					break;
			}
			return next;
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
				//currentShape = new S_Shape(left + 3 * unitLength, top, unitLength);
				//currentShape = allShapes[random.Next()];4
				currentShape = getNextShape();
			}
		}

		internal void rotate()
		{
			var rotated = currentShape.rotate();
			if (isSpaceToRotate(rotated))
			{
				currentShape = rotated;
			}
		}

		private bool isSpaceToRotate(Shape rotated)
		{
			foreach (var square in rotated.squares)
			{
				if (square != null)
				{
					if (!isSpace(square))
					{
						return false;
					}
				}
			}
			return true;
		}

		private bool isSpace(Square square)
		{
			var x = square.x;
			var y = square.y;
			if (x >= left && x < right && y >= top && y < bottom)
			{
				foreach (var sq in landedSquares)
				{
					if (sq != null && x == sq.x && y == sq.y)
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

		private void addShapeToLandedSquares()
		{
			foreach (var square in currentShape.squares)
			{
				if (square != null)
				{
					var xIndex = (square.x - left) / unitLength;
					var yIndex = (square.y - top) / unitLength;
					landedSquares[xIndex, yIndex] = square;
				}
			}
		}

		private bool isSpaceForShapeToDrop()
		{
			foreach (var square in currentShape.squares)
			{
				if (square != null)
				{
					if (!isSpaceToDrop(square))
					{
						return false;
					}
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
			if (isSpaceOnLeft())
			{
				currentShape.moveLeft();
			}
		}

		private bool isSpaceOnLeft()
		{
			foreach (var square in currentShape.squares)
			{
				if (square != null)
				{
					if (!isLeftFree(square))
					{
						return false;
					}
				}
			}
			return true;
		}

		private bool isLeftFree(Square square)
		{
			if (square.x > left)
			{
				foreach (var sq in landedSquares)
				{
					if (sq != null && sq.x == square.x - unitLength && sq.y == square.y)
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
			if (isSpaceOnRight())
			{
				currentShape.moveRight();
			}
		}

		private bool isSpaceOnRight()
		{
			foreach (var square in currentShape.squares)
			{
				if (square != null)
				{
					if (!isRightFree(square))
					{
						return false;
					}
				}
			}
			return true;
		}

		private bool isRightFree(Square square)
		{
			if (square.x + unitLength < right)
			{
				foreach (var sq in landedSquares)
				{
					if (sq != null && sq.x == square.x + unitLength && sq.y == square.y)
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

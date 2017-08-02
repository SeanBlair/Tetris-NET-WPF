using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Tetris.Shapes;

namespace Tetris
{
	class TetrisGame
	{
		// Classic tetris appears to be 10 square wide, and 18 square high.
		
		WriteableBitmap writeableBitmap;
		Shape currentShape;
		Square[,] landedSquares;
		public static int unitLength;
		static int left = 100;
		int right;
		static int top = 100;
		int bottom;
		Random random;
		public int score;


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
			score = 0;
		}

		private Shape getNextShape()
		{
			var startingX = left + 3 * unitLength;
			Shape next = null;
			switch (random.Next(7))
			{
				case 0:
					next = new S_Shape(startingX, top, unitLength);
					break;
				case 1:
					next = new T_Shape(startingX, top, unitLength);
					break;
				case 2:
					next = new Sb_Shape(startingX, top, unitLength);
					break;
				case 3:
					next = new L_Shape(startingX, top, unitLength);
					break;
				case 4:
					next = new Lb_Shape(startingX, top, unitLength);
					break;
				case 5:
					next = new O_Shape(startingX, top, unitLength);
					break;
				case 6:
					next = new I_Shape(startingX, top, unitLength);
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
				removeFullRows();
				currentShape = getNextShape();
			}
		}

		private void removeFullRows()
		{
			for (var i = 0; i < 18; i++)
			{
				if (isFullRow(i))
				{
					removeRow(i);
					dropLandedAbove(i);
					score++;
				}
			}
		}

		// modifies the y coordinates of all Squares above index
		// and drops all Square objects to correct location in landedSquares
		// (one lower).
		private void dropLandedAbove(int index)
		{
			for (var i = index - 1; i >= 0; i--)
			{
				for (var j = 0; j < 10; j++)
				{
					var square = landedSquares[j, i];
					if (square != null)
					{
						square.y += unitLength;
					}
					landedSquares[j, i + 1] = square; 
				}
			}
		}

		private void removeRow(int index)
		{
			for (var i = 0; i < 10; i++)
			{
				landedSquares[i, index] = null;
			}
		}

		private bool isFullRow(int index)
		{
			for (var i = 0; i < 10; i++)
			{
				if (landedSquares[i, index] == null)
				{
					return false;
				}
			}
			return true;
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

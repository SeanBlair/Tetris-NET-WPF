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
			theSquare = new Square(left + (5 * unitLength), top, Colors.Red);
		}

		public void render()
		{
			writeableBitmap.Clear();
			writeableBitmap.DrawRectangle(left, top, right, bottom, Colors.Black);
			theSquare.render(writeableBitmap);
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
			if (theSquare.y + unitLength < bottom)
			{
				theSquare.y += unitLength;
			}
			else
			{
				var xIndex = (theSquare.x - left) / unitLength;
				var yIndex = (theSquare.y - top) / unitLength;
				landedSquares[xIndex, yIndex] = theSquare;
				theSquare = new Square(left + (5 * unitLength), top, Colors.Red);
			}
		}

		internal void moveLeft()
		{
			if (theSquare.x > left)
			{
				theSquare.x -= unitLength;
			}
		}

		internal void moveRight()
		{
			if (theSquare.x + unitLength < right)
			{
				theSquare.x += unitLength;
			}
		}

		internal void moveDown()
		{
			if (theSquare.y + unitLength < bottom)
			{
				theSquare.y += unitLength;
			}
		}
	}
}

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
		public static int unitLength = 20;
		static int left = 100;
		static int right = left + (10 * unitLength);
		static int top = 100;
		static int bottom = top + (18 * unitLength);



		public TetrisGame(WriteableBitmap wb)
		{
			writeableBitmap = wb;
			isGameOver = false;
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

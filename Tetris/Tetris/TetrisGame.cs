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


		public TetrisGame(WriteableBitmap wb)
		{
			writeableBitmap = wb;
			isGameOver = false;
		}

		public void init()
		{
			theSquare = new Square(300, 100, Colors.Red);
		}

		public void render()
		{
			writeableBitmap.Clear();
			theSquare.render(writeableBitmap);
		}
	}
}

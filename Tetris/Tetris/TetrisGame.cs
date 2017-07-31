using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
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
		int millisecondsBetweenTick;
		int score;
		Square theSquare;


		public TetrisGame(WriteableBitmap wb)
		{
			writeableBitmap = wb;
			isGameOver = false;
			millisecondsBetweenTick = 1000;
		}

		public void init()
		{
			theSquare = new Square(300, 300, Colors.Red);
		}

		public void nextState()
		{
			theSquare.y += 10;
		}

		public void render()
		{
			theSquare.render(writeableBitmap);
		}

		public void tick()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			while (true)
			{
				if (stopwatch.ElapsedMilliseconds >= millisecondsBetweenTick)
				{
					return;
				}
				Thread.Sleep(1); //so processor can rest for a while
			}
		}
	}
}

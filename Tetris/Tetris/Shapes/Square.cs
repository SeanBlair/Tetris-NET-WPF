using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
	class Square
	{
		public int x;
		public int y;
		Color color;
		int length = TetrisGame.unitLength;

		public Square(int x, int y, Color color)
		{
			this.x = x;
			this.y = y;
			this.color = color;
		}

		internal void render(WriteableBitmap writeableBitmap)
		{
			writeableBitmap.FillRectangle(x, y, x + length, y + length, color);
			writeableBitmap.DrawRectangle(x, y, x + length, y + length, Colors.Black);
		}
	}
}

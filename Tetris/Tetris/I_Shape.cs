using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
	internal class I_Shape
	{
		public List<Square> squares;
		Color color = Colors.Yellow;
		int unitLength;

		public I_Shape(int x, int y, int unitLength)
		{
			this.unitLength = unitLength;
			squares = new List<Square>
			{
				new Square(x, y, color),
				new Square(x + unitLength, y, color),
				new Square(x + 2 * unitLength, y, color),
				new Square(x + 3 * unitLength, y, color)
			};
		}

		public void render(WriteableBitmap wb)
		{
			foreach (var square in squares)
			{
				square.render(wb);
			}
		}

		public void moveDown()
		{
			foreach (var square in squares)
			{
				square.y += unitLength;
			}
		}

		internal void moveLeft()
		{
			foreach (var square in squares)
			{
				square.x -= unitLength;
			}
		}

		internal void moveRight()
		{
			foreach (var square in squares)
			{
				square.x += unitLength;
			}
		}
	}
}
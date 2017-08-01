using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
	class T_Shape
	{
		public Square[,] squares;
		Color color = Colors.Red;
		int unitLength;

		public T_Shape(int x, int y, int unitLength)
		{
			this.unitLength = unitLength;
			squares = new Square[,]
				{
					{ null, new Square(x + unitLength, y, color), null },
					{ new Square(x, y + unitLength, color),
					new Square(x + unitLength, y + unitLength, color),
					new Square(x + 2 * unitLength, y + unitLength, color) },
					{null, null, null } };
		}

		public void render(WriteableBitmap wb)
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.render(wb);
				}
			}
		}

		public void moveDown()
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.y += unitLength;
				}
			}
		}

		internal void moveLeft()
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.x -= unitLength;
				}
			}
		}

		internal void moveRight()
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.x += unitLength;
				}
			}
		}

		// returns a shape rotated 90 degrees clockwise
		internal T_Shape rotate()
		{
			// Top left corrner coords of the squares 2d array.
			var x = squares[1, 1].x - unitLength;
			var y = squares[1, 1].y - unitLength;
			T_Shape rotated = new T_Shape(0, 0, unitLength);
			Square[,] flipped = new Square[3, 3];

			for (var i = 0; i < 3; i++)
			{
				for (var j = 0; j < 3; j++)
				{
					Square sqr = null;
					if (squares[i, j] != null)
					{
						// Adjusts the coords to implement a 90 degree clockwise rotation.
						sqr = new Square(x + ((2 - i) * unitLength), y + (j * unitLength), color);
					}
					flipped[j, 2 - i] = sqr;
				}
			}
			rotated.squares = flipped;
			return rotated;
		}

		// Dumb implementation of rotate that showed the logic implemented above...
		// TODO eliminate.
		internal T_Shape rotateDumb()
		{
			// need to rotate 3x3 matrix.
			// Squares need to have there x, y's adjusted.
			// top left corner of the 2d array is squares[1,1].x - unitLength, squares[1,1].y - unitLength
			// this is because the middle is guaranteed to have a Square object.
			var x1 = squares[1, 1].x;
			var y1 = squares[1, 1].y;
			var x2 = x1 + unitLength;
			var y2 = y1 + unitLength;
			var x0 = x1 - unitLength;
			var y0 = y1 - unitLength;

			T_Shape rotated = new T_Shape(0, 0, unitLength);
			Square[,] flipped = new Square[3, 3];

			if (squares[0, 0] != null)
			{
				flipped[0, 2] = new Square(x2, y0, color);
			}
			else
			{
				flipped[0, 2] = null;
			}
			if (squares[0, 1] != null)
			{
				flipped[1, 2] = new Square(x2, y1, color);
			}
			else
			{
				flipped[1, 2] = null;
			}
			if (squares[0, 2] != null)
			{
				flipped[2, 2] = new Square(x2, y2, color);
			}
			else
			{
				flipped[2, 2] = null;
			}
			if (squares[1, 0] != null)
			{
				flipped[0, 1] = new Square(x1, y0, color);
			}
			else
			{
				flipped[0, 1] = null;
			}
			flipped[1, 1] = new Square(x1, y1, color);
			if (squares[1, 2] != null)
			{
				flipped[2, 1] = new Square(x1, y2, color);
			}
			else
			{
				flipped[2, 1] = null;
			}
			if (squares[2, 0] != null)
			{
				flipped[0, 0] = new Square(x0, y0, color);
			}
			else
			{
				flipped[0, 0] = null;
			}
			if (squares[2, 1] != null)
			{
				flipped[1, 0] = new Square(x0, y1, color);
			}
			else
			{
				flipped[1, 0] = null;
			}
			if (squares[2, 2] != null)
			{
				flipped[2, 0] = new Square(x0, y2, color);
			}
			else
			{
				flipped[2, 0] = null;
			}
			rotated.squares = flipped;
			return rotated;
		}
	}
}

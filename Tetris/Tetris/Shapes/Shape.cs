using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris.Shapes
{
    class Shape
    {
		public Square[,] squares;
		public int unitLength;
		public Color color;

		public virtual void render(WriteableBitmap wb)
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.render(wb);
				}
			}
		}

		public virtual void moveDown()
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.y += unitLength;
				}
			}
		}

		public virtual void moveLeft()
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.x -= unitLength;
				}
			}
		}

		public virtual void moveRight()
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
		public virtual Shape rotate()
		{
			// Top left corrner coords of the squares 2d array.
			var x = squares[1, 1].x - unitLength;
			var y = squares[1, 1].y - unitLength;
			Shape rotated = new Shape { unitLength = this.unitLength, color = this.color };
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
	}
}

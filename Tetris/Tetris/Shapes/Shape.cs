using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris.Shapes
{
    class Shape
    {
		public Square[,] squares;
		public int unitLength;
		public Color color;

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

		public void moveLeft()
		{
			foreach (var square in squares)
			{
				if (square != null)
				{
					square.x -= unitLength;
				}
			}
		}

		public void moveRight()
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
		public Shape rotate()
		{
			// Top left corner coords of the squares 2d array.
			int x;
			int y;
			// one of these 2 locations will always have a Square object.
			if (squares[1, 1] != null)
			{
				x = squares[1, 1].x - unitLength;
				y = squares[1, 1].y - unitLength;
			}
			else
			{
				x = squares[2, 2].x - 2 * unitLength;
				y = squares[2, 2].y - 2 * unitLength;
			}

			Shape rotated = new Shape { unitLength = this.unitLength, color = this.color };
			var arrayLength = this.squares.GetLength(0);
			Square[,] flipped = new Square[arrayLength, arrayLength];

			for (var i = 0; i < arrayLength; i++)
			{
				for (var j = 0; j < arrayLength; j++)
				{
					Square sqr = null;
					if (squares[i, j] != null)
					{
						// Adjusts the coords to implement a 90 degree clockwise rotation.
						sqr = new Square(x + ((arrayLength - 1 - i) * unitLength), y + (j * unitLength), color);
					}
					// places sqr in location 90 degrees clockwise from original location of 2D array.
					flipped[j, arrayLength - 1 - i] = sqr;
				}
			}
			rotated.squares = flipped;
			return rotated;
		}
	}
}

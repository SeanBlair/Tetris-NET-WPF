using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
	internal class I_ShapeOriginal
	{
		enum Rotation {zero, ninety};

		public Square[] squares;
		Color color = Colors.Yellow;
		int unitLength;
		Rotation rotation;

		public I_ShapeOriginal(int x, int y, int unitLength)
		{
			this.unitLength = unitLength;
			squares = new Square[]
			{
				new Square(x, y, color),
				new Square(x + unitLength, y, color),
				new Square(x + 2 * unitLength, y, color),
				new Square(x + 3 * unitLength, y, color)
			};
			rotation = Rotation.zero;
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

		internal I_ShapeOriginal rotate()
		{
			Square[] rotated = new Square[4];
			Square[] original = this.squares;
			I_ShapeOriginal rotatedI_Shape = new I_ShapeOriginal(0, 0, unitLength);

			Square square;
			switch (rotation)
			{
				case Rotation.zero:
					{
						square = new Square(original[0].x + 2 * unitLength, original[0].y - unitLength, color);
						rotated[0] = square;

						square = new Square(original[1].x + unitLength, original[1].y, color);
						rotated[1] = square;

						square = new Square(original[2].x, original[2].y + unitLength, color);
						rotated[2] = square;

						square = new Square(original[3].x - unitLength, original[3].y + 2 * unitLength, color);
						rotated[3] = square;

						rotatedI_Shape.squares = rotated;
						rotatedI_Shape.rotation = Rotation.ninety;
						break;
					}
				case Rotation.ninety:
					{
						square = new Square(original[0].x - 2 * unitLength, original[0].y + unitLength, color);
						rotated[0] = square;

						square = new Square(original[1].x - unitLength, original[1].y, color);
						rotated[1] = square;

						square = new Square(original[2].x, original[2].y - unitLength, color);
						rotated[2] = square;

						square = new Square(original[3].x + unitLength, original[3].y - 2 * unitLength, color);
						rotated[3] = square;

						rotatedI_Shape.squares = rotated;
						rotatedI_Shape.rotation = Rotation.zero;
						break;
					}
			}
			return rotatedI_Shape;
		}
	}
}
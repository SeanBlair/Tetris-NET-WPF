using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris.Shapes
{
    class I_Shape : Shape 
    {
		enum Rotation { zero, ninety };

		// hiding base.squares as there is different logic to this shape.
		new Square[] squares;
		Rotation rotation;

		public I_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Yellow;
			squares = new Square[]
			{
				new Square(x, y, color),
				new Square(x + unitLength, y, color),
				new Square(x + 2 * unitLength, y, color),
				new Square(x + 3 * unitLength, y, color)
			};
			rotation = Rotation.zero;
		}



		public override void render(WriteableBitmap wb)
		{
			foreach (var square in squares)
			{
				square.render(wb);
			}
		}

		public override void moveDown()
		{
			foreach (var square in squares)
			{
				square.y += unitLength;
			}
		}

		public override void moveLeft()
		{
			foreach (var square in squares)
			{
				square.x -= unitLength;
			}
		}

		public override void moveRight()
		{
			foreach (var square in squares)
			{
				square.x += unitLength;
			}
		}

		public override Shape rotate()
		{
			Square[] rotated = new Square[4];
			Square[] original = this.squares;
			I_Shape rotatedShape = new I_Shape(0, 0, this.unitLength);

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

						rotatedShape.squares = rotated;
						rotatedShape.rotation = Rotation.ninety;
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

						rotatedShape.squares = rotated;
						rotatedShape.rotation = Rotation.zero;
						break;
					}
			}
			return rotatedShape;
		}
	}
}

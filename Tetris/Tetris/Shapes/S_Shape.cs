using System.Windows.Media;

namespace Tetris.Shapes
{
    class S_Shape : Shape
    {
		public S_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Blue;
			base.squares = new Square[,]
			{
				{ null, new Square(x + unitLength, y, color), new Square(x + 2 * unitLength, y, color)},
				{ new Square(x, y + unitLength, color), new Square(x + unitLength, y + unitLength, color), null },
				{null, null, null }
			};
		}
	}
}

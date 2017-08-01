using System.Windows.Media;

namespace Tetris.Shapes
{
    class Sb_Shape : Shape
    {
		public Sb_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Maroon;
			base.squares = new Square[,]
			{
				{ new Square(x, y, color), new Square(x + unitLength, y, color), null },
				{ null, new Square(x + unitLength, y + unitLength, color), new Square(x + 2 * unitLength, y + unitLength, color)},
				{null, null, null }
			};
		}
	}
}

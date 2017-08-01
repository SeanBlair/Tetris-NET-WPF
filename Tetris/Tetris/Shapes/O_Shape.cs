using System.Windows.Media;

namespace Tetris.Shapes
{
    class O_Shape : Shape
    {
		public O_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Orange;
			base.squares = new Square[,]
			{
				{ new Square(x, y, color), new Square(x + unitLength, y, color), null },
				{ new Square(x, y + unitLength, color), new Square(x + unitLength, y + unitLength, color), null },
				{null, null, null }
			};
		}
	}
}

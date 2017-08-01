using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris.Shapes
{
    class I_Shape : Shape 
    {
		public I_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Yellow;
			base.squares = new Square[,]
			{
				{null, null, null, null },
			    { new Square(x, y + unitLength, color),
			      new Square(x + unitLength, y + unitLength, color),
			      new Square(x + 2 * unitLength, y + unitLength, color),
			      new Square(x + 3 * unitLength, y + unitLength, color)},
			    {null, null, null, null },
			    {null, null, null, null }
			};
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris.Shapes
{
    class T_Shape : Shape
    {
		public T_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Red;
			base.squares = new Square[,]
			{
				{ null, new Square(x + unitLength, y, color), null },
				{ new Square(x, y + unitLength, color),
				new Square(x + unitLength, y + unitLength, color),
				new Square(x + 2 * unitLength, y + unitLength, color) },
				{null, null, null }
			};
		}
	}
}


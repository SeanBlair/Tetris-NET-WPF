using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris.Shapes
{
    class L_Shape : Shape
    {
		public L_Shape(int x, int y, int unitLength)
		{
			base.unitLength = unitLength;
			base.color = Colors.Pink;
			base.squares = new Square[,]
			{
				{ null, null, new Square(x + 2 * unitLength, y, color) },
				{ new Square(x, y + unitLength, color),
				  new Square(x + unitLength, y + unitLength, color),
				  new Square(x + 2 * unitLength, y + unitLength, color) },
				{null, null, null }
			};
		}
	}
}

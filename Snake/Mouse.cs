using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Mouse : MovingEntity
    {
        public Mouse(Point newPosition) : base(newPosition) { }

        public Mouse(Point newPosition, TDirection newDirection) : base(newPosition, newDirection) { }
    }
}

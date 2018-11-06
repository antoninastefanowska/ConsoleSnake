using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Fruit : Entity
    {
        public Fruit(Point newPosition) : base(newPosition) { }

        public Point GetPosition()
        {
            return Elements[0].Position;
        }
    }
}

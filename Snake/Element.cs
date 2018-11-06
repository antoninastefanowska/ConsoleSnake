using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Element
    {
        public Point Position { get; set; }

        public Element(Point newPosition)
        {
            Position = newPosition;
        }
    }
}

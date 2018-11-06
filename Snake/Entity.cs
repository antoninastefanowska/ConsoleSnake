using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Entity
    {
        public List<Element> Elements { get; set; }

        public Entity()
        {
            Elements = new List<Element>();
        }

        // konstruktor dla obiektów jedno-elementowych
        public Entity(Point newPosition)
        {
            Elements = new List<Element>();
            Elements.Add(new Element(newPosition));
        }

        // konstruktor dla złożonych obiektów
        public Entity(List<Point> newPositions)
        {
            Elements = new List<Element>();
            for (int i = 0; i < newPositions.Count; i++)
                Elements.Add(new Element(newPositions[i]));
        }

        public Point GetPosition()
        {
            return Elements[0].Position;
        }

        public List<Point> GetOccupiedSpace()
        {
            List<Point> positions = new List<Point>();
            foreach (Element element in Elements)
                positions.Add(element.Position);
            return positions;
        }
    }
}

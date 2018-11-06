using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class MovingEntity : Entity
    {
        public enum TDirection
        {
            Up,
            Down,
            Left,
            Right,
        };

        public TDirection Direction { get; set; }
        public int Wait { get; set; }
        private int skip;

        public MovingEntity(Point newPosition) : base(newPosition)
        {
            Direction = TDirection.Left;
            Wait = 1;
            skip = 0;
        }

        public MovingEntity(Point newPosition, TDirection newDirection) : base(newPosition)
        {
            Direction = newDirection;
            Wait = 1;
            skip = 0;
        }

        public MovingEntity(Point newPosition, TDirection newDirection, int wait) : base(newPosition)
        {
            Direction = newDirection;
            Wait = wait;
            skip = 0;
        }

        public MovingEntity(List<Point> newPositions, TDirection newDirection) : base(newPositions)
        {
            Direction = newDirection;
            Wait = 1;
            skip = 0;
        }

        public MovingEntity(List<Point> newPositions, TDirection newDirection, int speed) : base(newPositions)
        {
            Direction = newDirection;
            Wait = speed;
            skip = 0;
        }

        /* przed wykonaniem funkcji trzeba sprawdzić czy ruch jest możliwy */
        /* specjalne przypadki: owoce i przeszkody - trzeba będzie obsłużyć */
        public void Move(Point newPosition)
        {
            for (int i = Elements.Count - 1; i > 0; i--)
                Elements[i].Position = Elements[i - 1].Position;
            Elements[0].Position = newPosition;
        }

        public Point CalculateNewPosition(int limitWidth, int limitHeight)
        {
            Point oldPosition = Elements[0].Position, newPosition = new Point();
            if (skip != 0)
            {
                skip = (skip + 1) % Wait;
                return oldPosition;
            }
            else
            {
                switch (Direction)
                {
                    case TDirection.Up:
                        newPosition = new Point(oldPosition.X - 1, oldPosition.Y);
                        break;
                    case TDirection.Down:
                        newPosition = new Point(oldPosition.X + 1, oldPosition.Y);
                        break;
                    case TDirection.Right:
                        newPosition = new Point(oldPosition.X, oldPosition.Y + 1);
                        break;
                    case TDirection.Left:
                        newPosition = new Point(oldPosition.X, oldPosition.Y - 1);
                        break;
                }

                if (newPosition.X >= limitHeight)
                    newPosition.X = 0;
                else if (newPosition.X < 0)
                    newPosition.X = limitHeight - 1;
                if (newPosition.Y >= limitWidth)
                    newPosition.Y = 0;
                else if (newPosition.Y < 0)
                    newPosition.Y = limitWidth - 1;

                return newPosition;
            }
        }
    }
}

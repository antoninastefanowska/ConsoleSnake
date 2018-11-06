using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Snake : MovingEntity
    {
        public Effect Effect { get; set; }

        public Snake(List<Point> newPositions, TDirection newDirection) : base(newPositions, newDirection)
        {
            Effect = new Effect();
        }

        public void EatFruit(Fruit fruit)
        {
            Elements.Insert(0, new Element(fruit.GetPosition()));
        }

        public void EatPowerup(Powerup powerup)
        {
            Effect = powerup.Effect;
        }

        public int Size()
        {
            return Elements.Count;
        }

        public void ChangeDirection(TDirection newDirection)
        {
            if (Direction == TDirection.Up && newDirection == TDirection.Down)
                return;
            if (Direction == TDirection.Down && newDirection == TDirection.Up)
                return;
            if (Direction == TDirection.Right && newDirection == TDirection.Left)
                return;
            if (Direction == TDirection.Left && newDirection == TDirection.Right)
                return;
            Direction = newDirection;
        }

        public void Teleport(Point newPosition)
        {
            Elements[0].Position = newPosition;
        }
    }
}

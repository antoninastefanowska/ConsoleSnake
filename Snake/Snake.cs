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

        public void EatMouse(Mouse mouse)
        {
            Elements.Insert(0, new Element(mouse.GetPosition()));
        }

        public void EatPowerup(Powerup powerup)
        {
            Effect = powerup.Effect;
            switch (Effect.Variant)
            {
                case Effect.EffectVariant.Fast:
                    Wait /= 2;
                    break;
                case Effect.EffectVariant.Slow:
                    Wait *= 2;
                    break;
                case Effect.EffectVariant.Shrink:
                    Elements.RemoveRange(Size() / 2, Size() / 2);
                    break;
            }
        }

        public void EatObstacle(Obstacle obstacle)
        {
            /* game over, chyba że efekt */
            if (Effect.Variant == Effect.EffectVariant.Invicible)
                return;
        }

        public void EatSelf()
        {
            for (int i = 1; i < Size(); i++)
            {
                if (Elements[i].Position.Equals(Elements[0].Position))
                {
                    Elements.RemoveRange(i, Size() - i);
                    break;
                }
            }
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

        public void ChangeSpeed(int newWait)
        {
            Wait = newWait;
        }

        public void Teleport(Point newPosition)
        {
            Elements[0].Position = newPosition;
        }
    }
}

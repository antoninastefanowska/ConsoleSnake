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
        public int Lives { get; set; }
        public int Score { get; set; }

        public Snake(List<Point> newPositions, TDirection newDirection) : base(newPositions, newDirection)
        {
            Effect = new Effect();
            Lives = 3;
            Score = 0;
        }

        public void EatFruit(Point position)
        {
            Elements.Insert(0, new Element(position));
            Score += 50;
        }

        public void EatMouse(Point position)
        {
            Elements.Insert(0, new Element(position));
            Score += 200;
        }

        public void EatPowerup(Point position, Effect effect)
        {
            Effect = effect;
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
                    Effect.ResetEffect();
                    break;
                case Effect.EffectVariant.Life:
                    Lives++;
                    Effect.ResetEffect();
                    break;
            }
            Move(position);
        }

        public void EndEffect()
        {
            switch (Effect.Variant)
            {
                case Effect.EffectVariant.Fast:
                    Wait = 2;
                    break;
                case Effect.EffectVariant.Slow:
                    Wait = 2;
                    break;
            }
            Effect.ResetEffect();
        }

        public bool IsEffectActive()
        {
            if (Effect.Variant == Effect.EffectVariant.None)
                return false;
            else
                return true;
        }

        public void EatObstacle(Point position)
        {
            if (Effect.Variant == Effect.EffectVariant.Invicible)
                return;
            else
            {
                Lives--;
                Score -= 25;
                Effect = new Effect(Effect.EffectVariant.Invicible, 20); 
            }
            Move(position);
        }

        public void EatSelf(Point position)
        {
            Move(position);
            if (Effect.Variant == Effect.EffectVariant.Invicible)
                return;
            else
            {
                for (int i = 1; i < Size(); i++)
                {
                    if (Elements[i].Position.Equals(Elements[0].Position))
                    {
                        Score -= (Size() - i) * 50;
                        Elements.RemoveRange(i, Size() - i);
                        break;
                    }
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

        public void Teleport(Point newPosition)
        {
            Elements[0].Position = newPosition;
        }

        public Point CalculateNewPosition(int limitWidth, int limitHeight)
        {
            Point oldPosition = Elements[0].Position, newPosition = new Point();
            if ((skip = (skip + 1) % Wait) != 0)
                return oldPosition;
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

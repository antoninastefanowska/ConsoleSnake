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

        public void EatFruit(Fruit fruit)
        {
            Elements.Insert(0, new Element(fruit.GetPosition()));
            Score += 50;
        }

        public void EatMouse(Mouse mouse)
        {
            Elements.Insert(0, new Element(mouse.GetPosition()));
            Score += 200;
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
                    Effect.ResetEffect();
                    break;
                case Effect.EffectVariant.Life:
                    Lives++;
                    Effect.ResetEffect();
                    break;
            }
            Move(powerup.GetPosition());
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
    }
}

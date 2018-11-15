using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class ConsoleMapBuilder : IMapBuilder
    {
        private int x, y;

        public ConsoleMapBuilder(int mapWidth, int mapHeight)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < mapHeight; i++)
                Console.WriteLine(new string(' ', mapWidth));
            x = mapHeight;
            y = mapWidth;
        }

        public void ClearMap(List<Point> clearedPositions)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Point position in clearedPositions)
            {
                Console.SetCursorPosition(position.Y, position.X);
                Console.Write(' ');
            }
        }

        public void BuildFruit(Fruit fruit)
        {
            int x = fruit.GetPosition().X, y = fruit.GetPosition().Y;
            Console.SetCursorPosition(y, x);
            Console.Write('o');
        }

        public void BuildMouse(Mouse mouse)
        {
            int x = mouse.GetPosition().X, y = mouse.GetPosition().Y;
            Console.SetCursorPosition(y, x);
            Console.Write(Convert.ToChar(9733));
        }

        public void BuildObstacle(Obstacle obstacle)
        {
            foreach (Element element in obstacle.Elements)
            {
                Console.SetCursorPosition(element.Position.Y, element.Position.X);
                Console.Write(Convert.ToChar(9932));
            }
        }

        public void BuildPowerup(Powerup powerup)
        {
            int x = powerup.GetPosition().X, y = powerup.GetPosition().Y;
            Console.SetCursorPosition(y, x);
            char c;

            switch (powerup.Effect.Variant)
            {
                case Effect.EffectVariant.Invicible:
                    c = Convert.ToChar(9960);
                    break;
                case Effect.EffectVariant.Fast:
                    c = Convert.ToChar(9193);
                    break;
                case Effect.EffectVariant.Slow:
                    c = Convert.ToChar(9194);
                    break;
                case Effect.EffectVariant.Shrink:
                    c = Convert.ToChar(9986);
                    break;
                case Effect.EffectVariant.Life:
                    c = Convert.ToChar(9829);
                    break;
                default:
                    c = Convert.ToChar(0x2605);
                    break;
            }
            Console.Write(c);
        }

        public void BuildSnake(Snake snake)
        {
            char c;
            switch (snake.Effect.Variant) // na podstawie aktywnego efektu wąż zmienia wygląd
            {
                case Effect.EffectVariant.Invicible:
                    c = Convert.ToChar(0x2592);
                    break;
                default:
                    c = Convert.ToChar(0x2588);
                    break;
            }
            Console.SetCursorPosition(snake.GetPosition().Y, snake.GetPosition().X);
            Console.Write(c);
        }
    }
}

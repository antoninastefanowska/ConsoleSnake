using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class ConsoleMapBuilder : IMapBuilder
    {
        private int x, y;
        private char[,] plansza;
        public char[,] Plansza
        {
            get
            {
                return plansza;
            }
        }

        public ConsoleMapBuilder(Map map)
        {
            plansza = new char[map.Height, map.Width];
            x = map.Height;
            y = map.Width;
            ClearMap();
        }

        public void ClearMap()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                    plansza[i, j] = ' ';
            }
        }

        public void BuildFruit(Fruit fruit)
        {
            int x = fruit.GetPosition().X, y = fruit.GetPosition().Y;
            plansza[x, y] = 'o';
        }

        public void BuildMouse(Mouse mouse)
        {
            throw new NotImplementedException();
        }

        public void BuildObstacle(Obstacle obstacle)
        {
            throw new NotImplementedException();
        }

        public void BuildPowerup(Powerup powerup)
        {
            int x = powerup.GetPosition().X, y = powerup.GetPosition().Y;
            plansza[x, y] = Convert.ToChar(0x2605);
        }

        public void BuildProjectile(Projectile projectile)
        {
            throw new NotImplementedException();
        }

        public void BuildSnake(Snake snake)
        {
            List<Element> elements = snake.Elements;
            char c;
            switch (snake.Effect.Variant)
            {
                case Effect.EffectVariant.Invicible:
                    c = Convert.ToChar(0x2592);
                    break;
                default:
                    c = Convert.ToChar(0x2588);
                    break;
            }
            foreach (Element element in elements)
            {
                plansza[element.Position.X, element.Position.Y] = c;
            }
        }

        public void Print()
        {
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                    Console.Write(plansza[i, j]);
                Console.WriteLine();
            }
            Console.ResetColor();
            ClearMap();
        }
    }
}

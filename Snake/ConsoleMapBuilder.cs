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

        public void BuildMouse(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void BuildObstacleSegment(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void BuildPowerup(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void BuildProjectile(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void BuildSnake(Snake snake)
        {
            List<Element> elements = snake.Elements;
            foreach (Element element in elements)
                plansza[element.Position.X, element.Position.Y] = Convert.ToChar(0x2588); //Convert.ToChar(9744); //Convert.ToChar(9608);
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

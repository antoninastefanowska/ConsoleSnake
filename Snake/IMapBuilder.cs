using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public interface IMapBuilder
    {
        void BuildObstacleSegment(int x, int y);
        void BuildFruit(Fruit fruit);
        void BuildPowerup(int x, int y);
        void BuildProjectile(int x, int y);
        void BuildMouse(int x, int y);
        void BuildSnake(Snake snake);
        void Print();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public interface IMapBuilder
    {
        void ClearMap(List<Point> clearedPositions);
        void BuildObstacle(Obstacle obstacle);
        void BuildFruit(Fruit fruit);
        void BuildPowerup(Powerup powerup);
        void BuildMouse(Mouse mouse);
        void BuildSnake(Snake snake);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public interface IMapBuilder
    {
        void BuildObstacle(Obstacle obstacle);
        void BuildFruit(Fruit fruit);
        void BuildPowerup(Powerup powerup);
        void BuildProjectile(Projectile projectile);
        void BuildMouse(Mouse mouse);
        void BuildSnake(Snake snake);
        void Print();
    }
}

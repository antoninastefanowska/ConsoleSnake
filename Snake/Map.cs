using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Map
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Entity> Entities { get; set; }
        public Snake Snake { get; set; }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            Entities = new List<Entity>();
            GenerateSnake();
            GenerateFruit();
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            Entities.Remove(entity);
        }

        public void Update()
        {
            Point newPosition = Snake.CalculateNewPosition(Width, Height);
            Entity collisionEntity = EntityOccupyingPosition(newPosition);

            if (collisionEntity == null)
                Snake.Move(newPosition);
            else
            {
                if (collisionEntity is Fruit)
                {
                    Snake.EatFruit((Fruit)collisionEntity);
                    Entities.Remove(collisionEntity);
                    GenerateFruit();
                    /* zdobycie jakichś punktów */
                }
                if (collisionEntity is Obstacle)
                {
                    /* game over */
                }
                if (collisionEntity is Snake)
                {
                    /* zjedzenie kawałka siebie */
                }
                if (collisionEntity is Powerup)
                {
                    //smth
                }
                /* ... */
            }

            /* tu poniżej trzeba będzie obsłużyć ruch pocisków */
            /*
            foreach (Entity entity in Entities)
            {
                if (entity is MovingEntity)
                {
                    MovingEntity movingEntity = (MovingEntity)entity;
                    Point newPosition = movingEntity.CalculateNewPosition();

                    movingEntity.Move(newPosition);
                }
            } */
        }

        public Entity EntityOccupyingPosition(Point position)
        {
            /* wziąć pod uwagę jeszcze snake'a */
            foreach(Entity entity in Entities)
            {
                List<Point> occupiedPositions = entity.GetOccupiedSpace();
                foreach (Point occupiedPosition in occupiedPositions)
                    if ((position.X == occupiedPosition.X) && (position.Y == occupiedPosition.Y))
                        return entity;
            }
            foreach (Point occupiedPosition in Snake.GetOccupiedSpace())
                if ((position.X == occupiedPosition.X) && (position.Y == occupiedPosition.Y))
                    return Snake;
            return null;
        }

        public void GenerateFruit()
        {
            Random random = new Random();
            Point position;
            do
            {
                position = new Point(random.Next(Height), random.Next(Width));
            } while (EntityOccupyingPosition(position) != null);

            Fruit fruit = new Fruit(position);
            AddEntity(fruit);
        }

        public void GenerateObstacle()
        {

        }

        public void GenerateSnake()
        {
            //Snake snake;
            List<Point> positions = new List<Point>();
            positions.Add(new Point(5, 7));
            positions.Add(new Point(5, 8));
            positions.Add(new Point(5, 9));

            Snake = new Snake(positions, MovingEntity.TDirection.Left);
            //Entities.Add(snake);
        }
    }
}

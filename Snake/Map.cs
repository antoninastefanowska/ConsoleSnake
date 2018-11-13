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
        public int Score { get; set; }

        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            Entities = new List<Entity>();
            GenerateSnake();
            GenerateObstacle();
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
                    Snake.EatFruit(collisionEntity.GetPosition());
                    Entities.Remove(collisionEntity);
                    GenerateFruit();
                }
                else if (collisionEntity is Mouse)
                {
                    Snake.EatMouse(collisionEntity.GetPosition());
                    Entities.Remove(collisionEntity);
                }
                else if (collisionEntity is Obstacle)
                {
                    Snake.EatObstacle(collisionEntity.GetPosition());
                    /* sprawdzenie, czy zużyte zostały wszystkie życia - ewentualnie game over */
                }
                else if (collisionEntity is Snake)
                    Snake.EatSelf(newPosition);

                else if (collisionEntity is Powerup)
                {
                    Snake.EatPowerup(collisionEntity.GetPosition(), ((Powerup)collisionEntity).Effect);
                    Entities.Remove(collisionEntity);
                }
                /* ... */
            }

            /* tu poniżej trzeba będzie obsłużyć ruch pocisków */
            bool powerupExists = false, mouseExists = false;
            for (int i = 0; i < Entities.Count; i++)
            {
                /*
                if (entity is MovingEntity)
                {
                    MovingEntity movingEntity = (MovingEntity)entity;
                    Point newPosition = movingEntity.CalculateNewPosition();

                    movingEntity.Move(newPosition);
                } */
                if (Entities[i] is Powerup)
                {
                    Powerup powerup = (Powerup)Entities[i];
                    if (powerup.SpawnDuration > 0)
                    {
                        powerup.LowerDuration();
                        powerupExists = true;
                    }
                    else
                    {
                        Entities.Remove(powerup);
                        powerupExists = false;
                    }
                }

                if (Entities[i] is Mouse)
                {
                    Mouse mouse = (Mouse)Entities[i];
                    Point newMousePosition = mouse.CalculateNewPosition();
                    System.Diagnostics.Debug.WriteLine(newMousePosition.ToString());
                    if (EntityOccupyingPosition(newMousePosition) is Obstacle || newMousePosition.X >= Height || newMousePosition.Y >= Width || newMousePosition.X < 0 || newMousePosition.Y < 0)
                        Entities.Remove(mouse);
                    else
                    {
                        mouse.Move(newMousePosition);
                        mouseExists = true;
                    }
                }
            }

            if (!powerupExists && !Snake.IsEffectActive() && IsDrawn(20))
                GeneratePowerup();

            if (Snake.IsEffectActive() && Snake.Effect.Duration > 0)
                Snake.Effect.LowerDuration();
            else
                Snake.EndEffect();

            if (!mouseExists && IsDrawn(10))
                GenerateMouse();
        }

        public Entity EntityOccupyingPosition(Point position)
        {
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

        private bool IsDrawn(int percentage)
        {
            Random random = new Random();
            int los = random.Next(101);
            if (percentage > los)
                return true;
            else
                return false;
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

        public void GeneratePowerup()
        {
            Random random = new Random();
            Point position;
            Array values = Enum.GetValues(typeof(Effect.EffectVariant));
            Effect.EffectVariant effect;
            int los;
            do
            {
                position = new Point(random.Next(Height), random.Next(Width));
            } while (EntityOccupyingPosition(position) != null);
            los = random.Next(1, Effect.EffectNumber); /* to na później */
            //los = 3;
            effect = (Effect.EffectVariant)values.GetValue(los);

            Powerup powerup = new Powerup(position, effect, 80, 80);
            AddEntity(powerup);
        }

        public void GenerateObstacle()
        {
            List<Point> positions = new List<Point>();
            positions.Add(new Point(1, 7));
            positions.Add(new Point(1, 6));
            positions.Add(new Point(1, 5));

            Obstacle obstacle = new Obstacle(positions);
            Entities.Add(obstacle);
        }

        public void GenerateMouse()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(MovingEntity.TDirection));
            int los = random.Next(4);
            MovingEntity.TDirection direction = (MovingEntity.TDirection)values.GetValue(los);
            Mouse mouse = null;
            if (direction == MovingEntity.TDirection.Up || direction == MovingEntity.TDirection.Down)
            {
                los = random.Next(Height);
                mouse = new Mouse(new Point(los, 0), direction, 1);
            }
            else
            {
                los = random.Next(Width);
                mouse = new Mouse(new Point(0, los), direction, 1);
            }
            AddEntity(mouse);
        }

        public void GenerateSnake()
        {
            List<Point> positions = new List<Point>();
            positions.Add(new Point(5, 7));
            positions.Add(new Point(5, 8));
            positions.Add(new Point(5, 9));

            Snake = new Snake(positions, MovingEntity.TDirection.Left);
        }
    }
}

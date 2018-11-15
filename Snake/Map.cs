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
        public List<Point> ClearedPositions { get; set; }
        public int Stage { get; set; }
        public bool GameOver { get; set; }
        private int nextStage;
        private Random random;

        public Map(int height, int width, int stage)
        {
            random = new Random();
            GameOver = false;
            Height = height;
            Width = width;
            Entities = new List<Entity>();
            ClearedPositions = new List<Point>();
            GenerateSnake();
            GenerateFruit();
            Stage = stage;
            nextStage = stage;
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

            ClearedPositions.Clear();

            if (collisionEntity == null)
            {
                ClearedPositions.Add(Snake.Elements[Snake.Elements.Count - 1].Position);
                Snake.Move(newPosition);
            }
            else if (!newPosition.Equals(Snake.GetPosition()))
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
                    ClearedPositions.Add(Snake.Elements[Snake.Elements.Count - 1].Position);
                    Snake.EatObstacle(collisionEntity.GetPosition());
                    if (Snake.Lives <= 0)
                    {
                        GameOver = true;
                        return;
                    }
                }
                else if (collisionEntity is Snake)
                {
                    ClearedPositions.Add(Snake.Elements[Snake.Elements.Count - 1].Position);
                    List<Point> removedPositions = Snake.EatSelf(newPosition);
                    if (removedPositions != null)
                        foreach (Point position in removedPositions)
                            ClearedPositions.Add(position);
                }

                else if (collisionEntity is Powerup)
                {
                    Powerup powerup = (Powerup)collisionEntity;
                    ClearedPositions.Add(Snake.Elements[Snake.Elements.Count - 1].Position);
                    if (powerup.Effect.Variant == Effect.EffectVariant.Shrink)
                    {
                        List<Element> removedElements = Snake.Elements.GetRange(Snake.Size() / 2, Snake.Size() / 2 + Snake.Size() % 2);
                        foreach (Element element in removedElements)
                            ClearedPositions.Add(element.Position);
                    }
                    Snake.EatPowerup(collisionEntity.GetPosition(), ((Powerup)collisionEntity).Effect);
                    Entities.Remove(collisionEntity);
                }
            }
            List<Entity> removedEntities = new List<Entity>();
            bool powerupExists = false, mouseExists = false;
            for (int i = 0; i < Entities.Count; i++)
            {
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
                        ClearedPositions.Add(powerup.GetPosition());
                        removedEntities.Add(powerup);
                        powerupExists = false;
                    }
                }

                if (Entities[i] is Mouse)
                {
                    Mouse mouse = (Mouse)Entities[i];
                    Point newMousePosition = mouse.CalculateNewPosition();
                    ClearedPositions.Add(mouse.GetPosition());
                    if (newMousePosition.X >= Height || newMousePosition.Y >= Width || newMousePosition.X < 0 || newMousePosition.Y < 0)
                        removedEntities.Add(mouse);
                    else
                    {
                        mouse.Move(newMousePosition);
                        mouseExists = true;
                    }
                }
            }

            foreach (Entity entity in removedEntities)
                Entities.Remove(entity);

            if (!powerupExists && !Snake.IsEffectActive() && IsDrawn(1))
                GeneratePowerup();

            if (Snake.IsEffectActive() && Snake.Effect.Duration > 0)
                Snake.Effect.LowerDuration();
            else
                Snake.EndEffect();

            if (!mouseExists && IsDrawn(1))
                GenerateMouse();

            if (Snake.Score >= nextStage)
            {
                GenerateObstacle();
                nextStage += Stage;
            }
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
            int los = random.Next(101);
            if (percentage > los)
                return true;
            else
                return false;
        }

        public void GenerateFruit()
        {
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
            Point position;
            Array values = Enum.GetValues(typeof(Effect.EffectVariant));
            Effect.EffectVariant effect;
            int los;
            do
            {
                position = new Point(random.Next(Height), random.Next(Width));
            } while (EntityOccupyingPosition(position) != null);
            los = random.Next(1, Effect.EffectNumber);
            effect = (Effect.EffectVariant)values.GetValue(los);

            Powerup powerup = new Powerup(position, effect, 80, 80);
            AddEntity(powerup);
        }

        public void GenerateObstacle()
        {
            Point position;
            do
            {
                position = new Point(random.Next(Height), random.Next(Width));
            } while (EntityOccupyingPosition(position) != null);

            Obstacle obstacle = new Obstacle(position);
            Entities.Add(obstacle);
        }

        public void GenerateMouse()
        {
            Array values = Enum.GetValues(typeof(MovingEntity.TDirection));
            int los = random.Next(4);
            MovingEntity.TDirection direction = (MovingEntity.TDirection)values.GetValue(los);
            Mouse mouse = null;

            switch (direction)
            {
                case MovingEntity.TDirection.Up:
                    los = random.Next(Width);
                    mouse = new Mouse(new Point(Height - 1, los), direction, 1);
                    break;
                case MovingEntity.TDirection.Down:
                    los = random.Next(Width);
                    mouse = new Mouse(new Point(0, los), direction, 1);
                    break;
                case MovingEntity.TDirection.Left:
                    los = random.Next(Height);
                    mouse = new Mouse(new Point(los, Width - 1), direction, 1);
                    break;
                case MovingEntity.TDirection.Right:
                    los = random.Next(Height);
                    mouse = new Mouse(new Point(los, 0), direction, 1);
                    break;

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

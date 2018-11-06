using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace Snake
{
    class Program
    {
        private static Game newGame;
        private static IMapBuilder builder;

        static void Main(string[] args)
        {            
            Console.OutputEncoding = Encoding.Unicode;
            /*
            for (int i = 0; i < 10000; i++)
                Console.WriteLine(Convert.ToChar(i) + " " + i.ToString());
            Console.ReadKey(); */
            //LaunchMenu(Menu.Instance);
            LaunchGame();
        }

        static void LaunchMenu(Menu menu) // menu trzeba zbudować w klasie Menu
        {
            menu.Print();
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.GoUp();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.GoDown();
                        break;
                }
            }

            switch(menu.GetCurrentOption())
            {
                case "ROZPOCZNIJ GRĘ":
                    LaunchGame();
                    break;
                case "ZAKOŃCZ":
                    Environment.Exit(0);
                    break;
            }
        }

        static void LaunchGame()
        {
            newGame = Game.Instance; // globalny dostęp - niedobrze - trzeba będzie obiekt Game przekazać przez parametr
            newGame.GenerateMap(10, 20);
            Snake snake = newGame.Map.Snake;
            builder = new ConsoleMapBuilder(newGame.Map);

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.DownArrow:
                            snake.ChangeDirection(MovingEntity.TDirection.Down);
                            break;
                        case ConsoleKey.UpArrow:
                            snake.ChangeDirection(MovingEntity.TDirection.Up);
                            break;
                        case ConsoleKey.LeftArrow:
                            snake.ChangeDirection(MovingEntity.TDirection.Left);
                            break;
                        case ConsoleKey.RightArrow:
                            snake.ChangeDirection(MovingEntity.TDirection.Right);
                            break;
                    }
                }
                while (!Console.KeyAvailable)
                {
                    builder.BuildSnake(snake);
                    List<Entity> entities = newGame.Map.Entities;
                    foreach (Entity entity in entities)
                    {
                        if (entity is Fruit)
                            builder.BuildFruit((Fruit)entity);
                        if (entity is Powerup)
                            builder.BuildPowerup((Powerup)entity);
                    }
                    builder.Print();
                    newGame.Map.Update();
                    Thread.Sleep(250);
                }         
            }
        }

    }
}

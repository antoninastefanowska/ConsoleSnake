using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;
using System.Drawing;

namespace Snake
{
    class Program
    {
        private static IMapBuilder builder;
        private int wait, stage, mapHeight, mapWidth;
        private const int BANNER_HEIGHT = 7, BANNER_WIDTH = 34, WINDOW_HEIGHT = 20, WINDOW_WIDTH = 51;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;       
            Console.CursorVisible = false;
            
            while (true)
            {
                Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();
                Program P = new Program();
                P.Banner();
                P.LaunchMenu();
                P.LaunchGame(Game.Instance);
            }
        }

        private void PrintTextAnimated(string text)
        {
            string[] lines = text.Split('_');
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                if (!Console.KeyAvailable)
                    Thread.Sleep(50);
            }
        }

        public void Banner()
        {
            const string WELCOME =
            "                                                    _" +
            " .| '''| '||\\  ||`      /.\\     '||  //' '||''''| _" +
            " ||       ||\\  ||      // \\      || //    ||   .  _" +
            " `|'''|,  || \\ ||     //...\\     ||<<     ||'''|  _" +
            "  .   ||  ||  \\||    //     \\    || \\\\    ||      _" +
            "  |...|' .||  \\||. .//       \\. .||  \\\\. .||....| _" +
            "               wciśnij dowolny klawisz";

            PrintTextAnimated(WELCOME);
            Console.ReadKey();
            Console.SetCursorPosition(0, BANNER_HEIGHT);
            Console.Write(new string(' ', WINDOW_WIDTH));
        }

        public void LaunchMenu()
        {
            List<string> options = new List<string>();
            options.Add("ROZPOCZNIJ GRĘ");
            options.Add("ZAKOŃCZ");

            Menu menu = new Menu(options, "", new Point(BANNER_HEIGHT + 1, 2));
            menu.PrintTitle();
            menu.PrintOptions();
            menu.Choose();

            switch (menu.GetCurrentOption())
            {
                case "ROZPOCZNIJ GRĘ":
                    LaunchDifficultyMenu();
                    LaunchSpeedMenu();
                    LaunchSizeMenu();
                    break;
                case "ZAKOŃCZ":
                    Environment.Exit(0);
                    break;
            }
        }

        public void LaunchDifficultyMenu()
        {
            List<string> options = new List<string>();
            options.Add("ŁATWY");
            options.Add("NORMALNY");
            options.Add("TRUDNY");

            Menu menu = new Menu(options, "POZIOM TRUDNOŚCI", new Point(BANNER_HEIGHT + 1, 2));
            menu.PrintTitle();
            menu.PrintOptions();
            menu.Choose();

            switch (menu.GetCurrentOption())
            {
                case "ŁATWY":
                    stage = 500;
                    break;
                case "NORMALNY":
                    stage = 200;
                    break;
                case "TRUDNY":
                    stage = 100;
                    break;
            }
        }

        public void LaunchSpeedMenu()
        {
            List<string> options = new List<string>();
            options.Add("WOLNO");
            options.Add("NORMALNIE");
            options.Add("SZYBKO");

            Menu menu = new Menu(options, "PRĘDKOŚĆ", new Point(BANNER_HEIGHT + 1, 2));
            menu.PrintTitle();
            menu.PrintOptions();
            menu.Choose();

            switch (menu.GetCurrentOption())
            {
                case "WOLNO":
                    wait = 200;
                    break;
                case "NORMALNIE":
                    wait = 100;
                    break;
                case "SZYBKO":
                    wait = 50;
                    break;
            }
        }

        public void LaunchSizeMenu()
        {
            List<string> options = new List<string>();
            options.Add("40 x 20");
            options.Add("50 x 30");
            options.Add("100 x 40");

            Menu menu = new Menu(options, "WIELKOŚĆ MAPY", new Point(BANNER_HEIGHT + 1, 2));
            menu.PrintTitle();
            menu.PrintOptions();
            menu.Choose();

            switch (menu.GetCurrentOption())
            {
                case "40 x 20":
                    mapHeight = 20;
                    mapWidth = 40;
                    break;
                case "50 x 30":
                    mapHeight = 30;
                    mapWidth = 50;
                    break;
                case "100 x 40":
                    mapHeight = 40;
                    mapWidth = 100;
                    break;
            }
        }

        public void LaunchGame(Game newGame)
        {
            Console.SetWindowSize(mapWidth, mapHeight + 6);

            newGame.GenerateMap(mapHeight, mapWidth, stage);
            Snake snake = newGame.Map.Snake;
            builder = new ConsoleMapBuilder(mapWidth, mapHeight);

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, mapHeight);
            for (int i = 0; i < 6; i++)
                Console.WriteLine(new string(' ', mapWidth));

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
                    if (newGame.Map.GameOver)
                    {
                        Console.SetCursorPosition(mapWidth / 2 - 10, mapHeight / 2 - 2);
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(new string(' ', 20));
                        Console.SetCursorPosition(mapWidth / 2 - 10, mapHeight / 2 - 1);
                        Console.WriteLine("     KONIEC GRY     ");
                        Console.SetCursorPosition(mapWidth / 2 - 10, mapHeight / 2);
                        Console.WriteLine(new string(' ', 20));
                        string wynik = "WYNIK: " + newGame.Map.Snake.Score.ToString(), output = "";
                        output = new string(' ', (20 - wynik.Length) / 2) + wynik + new string(' ', (20 - wynik.Length) / 2 + (20 - wynik.Length) % 2);
                        Console.SetCursorPosition(mapWidth / 2 - 10, mapHeight / 2 + 1);
                        Console.WriteLine(output);
                        Console.SetCursorPosition(mapWidth / 2 - 10, mapHeight / 2 + 2);
                        Console.WriteLine(new string(' ', 20));
                        Console.ReadKey();
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;

                    List<Entity> entities = newGame.Map.Entities;
                    foreach (Entity entity in entities)
                    {
                        if (entity is Fruit)
                            builder.BuildFruit((Fruit)entity);
                        if (entity is Powerup)
                            builder.BuildPowerup((Powerup)entity);
                        if (entity is Obstacle)
                            builder.BuildObstacle((Obstacle)entity);
                        if (entity is Mouse)
                            builder.BuildMouse((Mouse)entity);
                    }
                    builder.BuildSnake(snake);

                    foreach (Point position in newGame.Map.ClearedPositions)
                        System.Diagnostics.Debug.WriteLine(position.ToString());
                    builder.ClearMap(newGame.Map.ClearedPositions);


                    Console.SetCursorPosition(2, mapHeight + 1);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write(new string(' ', mapWidth));
                    Console.SetCursorPosition(2, mapHeight + 1);
                    Console.Write(newGame.Map.Snake.Score.ToString());

                    Console.SetCursorPosition(mapWidth - 6, mapHeight + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(Convert.ToChar(9829));
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(" x " + newGame.Map.Snake.Lives.ToString());

                    if (newGame.Map.Snake.Effect.Variant != Effect.EffectVariant.None)
                    {
                        Console.SetCursorPosition(0, mapHeight + 3);
                        Console.WriteLine(new string(' ', mapWidth));

                        if (newGame.Map.Snake.Effect.Duration > 0)
                        {
                            Console.SetCursorPosition(2, mapHeight + 3);
                            Console.Write(newGame.Map.Snake.Effect.ToString());
                            Console.SetCursorPosition(mapWidth - 6, mapHeight + 3);
                            Console.Write(newGame.Map.Snake.Effect.Duration.ToString());
                        }
                    }

                    newGame.Map.Update();
                    Thread.Sleep(wait);
                }         
            }
        }

    }
}

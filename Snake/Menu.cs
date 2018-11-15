using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    public class Menu
    {
        public int CurrentOptionIndex { get; set; }
        public List<string> Options { get; set; }
        public string Title { get; set; }
        public Point Position { get; set; }

        public Menu(List<string> options, string title, Point position)
        {
            Options = options;
            Title = title;
            CurrentOptionIndex = 0;
            Position = position;
        }

        public void PrintTitle()
        {
            Console.SetCursorPosition(Position.Y, Position.X);
            Console.Write(new string(' ', 50));
            Console.SetCursorPosition(Position.Y, Position.X);
            Console.Write(new string(' ', 15) + Title);
        }

        public void PrintOptions()
        {
            
            for (int i = 0; i < Options.Count; i++)
            {
                Console.SetCursorPosition(Position.Y, Position.X + (i + 1) * 2);
                Console.Write(new string(' ', 15));
                Console.SetCursorPosition(Position.Y, Position.X + (i + 1) * 2);
                if (i == CurrentOptionIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.Write(Options[i]);
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        public void Choose()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        GoUp();
                        break;
                    case ConsoleKey.DownArrow:
                        GoDown();
                        break;
                }
                PrintOptions();
                keyInfo = Console.ReadKey();
            }
        }

        public void CheckOption(int index)
        {
            CurrentOptionIndex = index;
        }

        public string GetCurrentOption() { return Options[CurrentOptionIndex]; }

        public void GoUp() { CheckOption((CurrentOptionIndex - 1) % Options.Count); }

        public void GoDown() { CheckOption((CurrentOptionIndex + 1) % Options.Count); }
    }
}

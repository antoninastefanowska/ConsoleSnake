using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        private static Game instance;
        public Map Map { get; set; }
        public static Game Instance
        {
            get
            {
                if (instance == null)
                    instance = new Game();
                return instance;
            }
        }

        private Game() { }

        public void GenerateMap(int height, int width, int stage)
        {
            Map = new Map(height, width, stage);
        }
    }
}

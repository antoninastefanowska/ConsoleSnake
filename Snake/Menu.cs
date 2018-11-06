using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Menu
    {
        public int CurrentOptionIndex { get; set; }
        public List<string> Options { get; set; }
        public static Menu Instance
        {
            get
            {
                if (Instance == null)
                    return new Menu();
                else return Instance;
            }
        }

        private Menu()
        {
            Options = new List<string>();
            Options.Add("ROZPOCZNIJ GRĘ");
            Options.Add("ZAKOŃCZ GRĘ");
        }

        public void Print()
        {
            /* tutaj trzeba zbudować menu */
        }

        public void CheckOption(int index)
        {
            CurrentOptionIndex = index;
            /* wydrukować opcję jako podświetloną */
        }

        public string GetCurrentOption() { return Options[CurrentOptionIndex]; }

        public void GoUp() { CheckOption((CurrentOptionIndex - 1) % Options.Count); }

        public void GoDown() { CheckOption((CurrentOptionIndex + 1) % Options.Count); }
    }
}

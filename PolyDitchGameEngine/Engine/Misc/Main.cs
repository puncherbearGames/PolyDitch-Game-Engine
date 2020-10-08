using PolyDitchGameEngine.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDitchGameEngine.Engine.Misc
{
    public static class MainClass
    {
        public static BlankGame Game;
        static void Main(string[] args)
        {
            Game = new BlankGame(800, 800, "Blank Game");
        }
    }
}

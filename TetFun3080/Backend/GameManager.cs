using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public static class GameManager
    {
        public static TetFunGame game;

        public static void QuitGame()
        {
            //do some autosave funni
            game.Exit();
        }
    }
}

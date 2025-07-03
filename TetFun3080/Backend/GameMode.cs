using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    [Serializable]
    public class GameMode
    {
        public string Name { get; set; } = "Fallback"; // Name of the ruleset
        public string Description { get; set; } = "Oh! You're not supposed to see this.";
        public List<GameModeLevel> levels = new List<GameModeLevel>() { new GameModeLevel(0, new Ruleset()), new GameModeLevel(200, new Ruleset()) };


    }
    [Serializable]
    public class GameModeLevel
    {

        public GameModeLevel(int level, Ruleset ruleset)
        {
            Level = 0;
            Ruleset = new Ruleset();
        }

        public int Level { get; set; }
        public Ruleset Ruleset { get; set; } = new Ruleset(); // Ruleset for this level
    }

}

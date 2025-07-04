using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TetFun3080
{
    [Serializable]
    public class GameMode
    {
        public string Name { get; set; } = "Fallback"; // Name of the ruleset
        public string Description { get; set; } = "Oh! You're not supposed to see this.";
        public List<GameModeLevel> levels = new List<GameModeLevel>() { new GameModeLevel(0, new Ruleset()), new GameModeLevel(200, new Ruleset()) };


        public Ruleset GetRulesetFromLevel(int level)
        {
            GameModeLevel current = levels[0];

            foreach (GameModeLevel l in levels)
            {
                if (l.Level <= level && l.Level > current.Level)
                {
                    current = l;
                }
            }
            return current.Ruleset;

        }

    }
    [Serializable]
    public class GameModeLevel
    {

        public GameModeLevel(int level, Ruleset ruleset)
        {
            Level = level;
            Ruleset = ruleset;
        }

        public int Level { get; set; }
        public Ruleset Ruleset { get; set; } = new Ruleset(); // Ruleset for this level
    }

}

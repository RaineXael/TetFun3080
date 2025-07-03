using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace TetFun3080.Backend
{
    [Serializable]
    public class Ruleset
    {
        
        //Instances
        [JsonIgnore]
        public IRandomizer randomizer;
        [JsonIgnore]
        public IRotator rotator;

        public string RandomizerName;
        public string RotatorName;

        //Visual
        public int visibleNextCount = 5; // How many pieces to show in the next queue
        public bool ghostEnabled = true;

        //Gameplay
        public int initialLevel = 0;
        public int gravity = 10;
        public bool holdEnabled = true;
        public int appearanceDelay = 25; // Delay before the piece appears on the board
        public int lineclearDelay = 40; // Delay before lines are cleared
        public int dasTime = 16; // Delay before DAS activates
        public float lockInDelay = 3; // Delay before the piece locks in after hard drop / soft drop. Multiplied by gravity for true framecount.

        public bool InstalockOnHardDrop = false;

        public Ruleset()
        {
            //randomizer = new SevenBagRandomizer();
            randomizer = new SevenBagRandomizer();
            rotator = new RotatorQuick();
            //everything else set above
        }

    }
}

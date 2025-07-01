using System;
using System.IO;
using System.Xml.Serialization;
using TetFun3080.Backend;

namespace TetFun3080
{
    [Serializable]
    public class Ruleset
    {
        public string Name { get; set; } = "Fallback"; // Name of the ruleset
        //Instances
        private IRandomizer randomizer;
        private IRotator rotator;

        //Visual
        public int visibleNextCount = 5; // How many pieces to show in the next queue
        public bool ghostVisible = true;

        //Gameplay
        public int initialLevel = 0;
        public int initialGravity = 10;
        public bool holdEnabled = true;
        public int appearanceDelay = 15; // Delay before the piece appears on the board
        public int lineclearDelay = 25; // Delay before lines are cleared
        public int dasTime = 10; // Delay before DAS activates
        public float lockInDelay = 3; // Delay before the piece locks in after hard drop / soft drop. Multiplied by gravity for true framecount.

        public bool InstalockOnHardDrop = true;



        public Ruleset()
        {
            randomizer = new SevenBagRandomizer();
            rotator = new RotatorQuick();
            //everything else set above
        }

        public void LoadRulesetFromContent(string path)
        {
            //string path = Path.Combine("Content", "Rulesets", "DefaultRuleset.xml");

            if (!File.Exists(path))
            {
                Console.WriteLine("Ruleset XML not found. Using default values.");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Ruleset));
            using (FileStream stream = File.OpenRead(path))
            {
                Ruleset data = (Ruleset)serializer.Deserialize(stream);

                // Copy values to this Ruleset
                Name = data.Name;
                visibleNextCount = data.visibleNextCount;
                ghostVisible = data.ghostVisible;

                initialLevel = data.initialLevel;
                initialGravity = data.initialGravity;
                holdEnabled = data.holdEnabled;
                appearanceDelay = data.appearanceDelay;
                lineclearDelay = data.lineclearDelay;
                dasTime = data.dasTime;
                lockInDelay = data.lockInDelay;
                InstalockOnHardDrop = data.InstalockOnHardDrop;

                // Keep existing randomizer/rotator
            }

            Console.WriteLine($"Ruleset '{Name}' loaded from XML.");
        }

        public void SaveRulesetToFile(string filePath)
        {
            // Create a RulesetData from the current Ruleset values
            

            XmlSerializer serializer = new XmlSerializer(typeof(Ruleset));

            var settings = new System.Xml.XmlWriterSettings
            {
                Indent = true
            };

            using (var writer = System.Xml.XmlWriter.Create(filePath, settings))
            {
                serializer.Serialize(writer, this);
            }

        }
    }
}

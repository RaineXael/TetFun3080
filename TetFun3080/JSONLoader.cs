using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection.PortableExecutable;
using TetFun3080.Backend;

namespace TetFun3080
{
    public class JSONLoader
    {
        public Ruleset GetRulesetFromJSONFile(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);
                JObject jsonObject = JObject.Parse(jsonContent);
                Ruleset ruleset = jsonObject.ToObject<Ruleset>();
                return ruleset;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading ruleset from JSON file: {ex.Message}");
                return null;
            }
        }
        public void SaveRulesetToJSONFile(Ruleset ruleset, string filePath)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(ruleset, Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving ruleset to JSON file: {ex.Message}");
            }
        }

    
    }
}

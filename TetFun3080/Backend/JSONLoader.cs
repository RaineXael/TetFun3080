using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;

namespace TetFun3080.Backend
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

        public void SaveRulesetCollectionToJSONFile(List<Ruleset> rulesetCollection, string filePath)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(rulesetCollection, Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);
                Console.WriteLine($"Successfully saved ruleset collection to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving ruleset collection to JSON file: {ex.Message}");
            }
        }
        public void SaveGameModeToJSONFile(GameMode gamemode, string filePath)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(gamemode, Formatting.Indented);
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving gamemode to JSON file: {ex.Message}");
            }
        }

        internal GameMode LoadGameModeFromFile(string url)
        {
            try
            {
                string jsonContent = File.ReadAllText(url);
                JObject jsonObject = JObject.Parse(jsonContent);
                GameMode gamemode = jsonObject.ToObject<GameMode>();
                return gamemode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading gamemode from JSON file: {ex.Message}");
                return null;
            }
        }
    }
}

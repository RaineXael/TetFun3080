using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080
{
    public static class AssetManager
    {
        public const string customUserPath = "User/";
        public static GraphicsDeviceManager _graphics;
        public static ContentManager Content;
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> _audio = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, Effect> _effects = new Dictionary<string, Effect>();
        public static Texture2D fallbackTexture;
        public static SoundEffect fallbackSound;

        public static void LoadTexture(string assetName)
        {
            if (!_textures.ContainsKey(assetName))
            {
                try
                {
                    _textures.Add(assetName, Content.Load<Texture2D>(assetName));
                }
                catch (ContentLoadException)
                {
                    try
                    {
                        if (!_textures.ContainsKey(customUserPath + assetName))
                        {
                            FileStream fileStream = new FileStream(customUserPath + assetName, FileMode.Open);
                            _textures.Add(customUserPath + assetName, Texture2D.FromStream(_graphics.GraphicsDevice, fileStream));
                            fileStream.Dispose();
                        }
                    }
                    catch (Exception e)
                    {
                        // Handle the exception as needed, e.g., log it or rethrow it
                        Console.WriteLine($"Error loading texture '{assetName}': {e.Message}");
                       
                    }


                }
                


            }
        }

        public static Texture2D GetTexture(string assetName)
        {
            if (_textures.ContainsKey(assetName))
            {
                return _textures[assetName];
            }
            else if (_textures.ContainsKey(customUserPath + assetName))
            {
                return _textures[customUserPath + assetName];
            }
            else
            {
                // You might want to throw an exception or return a default texture
                DebugConsole.LogError($"Texture '{assetName}' not found in the AssetManager.");
                return fallbackTexture;
            }
        }



        public static void LoadAudio(string assetName)
        {
            try
            {
                if (!_audio.ContainsKey(assetName))
                {
                    _audio.Add(assetName, Content.Load<SoundEffect>(assetName));
                }
                else
                {
                    Console.WriteLine($"Error loading audio '{assetName}'");
                }
            }
            catch(ContentLoadException e)
            {
                Console.WriteLine($"Error loading audio '{assetName}'");
            }
        }


        public static SpriteFont GetFont(string assetName)
        {
            if (_fonts.ContainsKey(assetName))
            {
                return _fonts[assetName];
            }
            else
            {
                // You might want to throw an exception or return a default texture
                DebugConsole.LogError($"Font '{assetName}' not found in the AssetManager.");
                return null;
            }
        }
        public static void LoadFont(string assetName)
        {
            if (!_fonts.ContainsKey(assetName))
            {
                _fonts.Add(assetName, Content.Load<SpriteFont>(assetName));
            }
        }
        public static SoundEffect GetAudio(string assetName)
        {
            if (_audio.ContainsKey(assetName))
            {
                return _audio[assetName];
            }
            else
            {
                // You might want to throw an exception or return a default texture
                DebugConsole.LogError($"Sound '{assetName}' not found in the AssetManager.");
                return fallbackSound;
            }
        }

        public static Ruleset GetRuleset(string rulesetName)
        {
            //Searches the content folder for a ruleset JSON
            //if not found, search the User Ruleset folder for it

            //If unfound, return a default ruleset

            string contentPath = Path.Combine("Content", "Rulesets", rulesetName + ".json");
            string userPath = Path.Combine(customUserPath, "Rulesets", rulesetName + ".json");

            if (File.Exists(contentPath))
            {
                return new JSONLoader().GetRulesetFromJSONFile(contentPath);
            }
            else if (File.Exists(userPath))
            {
                return new JSONLoader().GetRulesetFromJSONFile(userPath);
            }
            else
            {
                Console.WriteLine($"Ruleset '{rulesetName}' not found. Using default ruleset.");
                return new Ruleset(); // Return a default ruleset
            }


        }

        public static void LoadEffect(string assetName)
        {
            if (!_effects.ContainsKey(assetName))
            {
                try
                {
                    _effects.Add(assetName, Content.Load<Effect>(assetName));
                }
                catch (ContentLoadException e)
                {

                    // Handle the exception as needed, e.g., log it or rethrow it
                    DebugConsole.LogError($"Error loading texture '{assetName}': {e.Message}");
                }
            }
        }

        public static Effect GetEffect(string assetName)
        {
            if (_effects.ContainsKey(assetName))
            {
                return _effects[assetName];
            }    
            else
            {
                // You might want to throw an exception or return a default texture
                DebugConsole.LogError($"Texture '{assetName}' not found in the AssetManager.");
                return null;
            }
        }
    }
}

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

namespace TetFun3080
{
    public static class AssetManager
    {
        public const string customUserPath = "User/";
        public static GraphicsDeviceManager _graphics;
        public static ContentManager Content;
        private static Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, Texture2D> _userTextures = new Dictionary<string, Texture2D>();
        private static Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();
        private static Dictionary<string, SoundEffect> _audio = new Dictionary<string, SoundEffect>();



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
                    FileStream fileStream = new FileStream(customUserPath + assetName, FileMode.Open);
                    _textures.Add(customUserPath + assetName, Texture2D.FromStream(_graphics.GraphicsDevice, fileStream));
                    fileStream.Dispose();
                }
                catch (Exception e)
                {
                    // Handle the exception as needed, e.g., log it or rethrow it
                    Console.WriteLine($"Error loading texture '{assetName}': {e.Message}");
                    throw;
                }


            }
        }




        public static void LoadFont(string assetName)
        {
            if (!_textures.ContainsKey(assetName))
            {
                _fonts.Add(assetName, Content.Load<SpriteFont>(assetName));
            }
        }
        public static void LoadAudio(string assetName)
        {
            if (!_textures.ContainsKey(assetName))
            {
                _audio.Add(assetName, Content.Load<SoundEffect>(assetName));
            }
        }

        public static Texture2D GetTexture(string assetName)
        {
            if (_textures.ContainsKey(assetName))
            {
                return _textures[assetName];
            }
            else if(_textures.ContainsKey(customUserPath + assetName))
            {
                return _textures[customUserPath + assetName];
            }
            else
            {
                // You might want to throw an exception or return a default texture
                throw new Exception($"Texture '{assetName}' not found in the AssetManager.");
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
                throw new Exception($"Font '{assetName}' not found in the AssetManager.");
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
                throw new Exception($"Sound '{assetName}' not found in the AssetManager.");
            }
        }
    }
}

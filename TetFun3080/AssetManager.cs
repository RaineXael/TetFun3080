using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080
{
    public class AssetManager
    {
        private ContentManager _content;
        private Dictionary<string, Texture2D> _textures;
        private Dictionary<string, SpriteFont> _fonts; 
        public AssetManager(ContentManager content)
        {
            _content = content;
            _textures = new Dictionary<string, Texture2D>();
            _fonts = new Dictionary<string, SpriteFont>();
        }

        public void LoadTexture(string assetName)
        {
            if (!_textures.ContainsKey(assetName))
            {
                _textures.Add(assetName, _content.Load<Texture2D>(assetName));
            }
        }
        public void LoadFont(string assetName)
        {
            if (!_textures.ContainsKey(assetName))
            {
                _fonts.Add(assetName, _content.Load<SpriteFont>(assetName));
            }
        }

        public Texture2D GetTexture(string assetName)
        {
            if (_textures.ContainsKey(assetName))
            {
                return _textures[assetName];
            }
            else
            {
                // You might want to throw an exception or return a default texture
                throw new Exception($"Texture '{assetName}' not found in the AssetManager.");
            }
        }
        public SpriteFont GetFont(string assetName)
        {
            if (_fonts.ContainsKey(assetName))
            {
                return _fonts[assetName];
            }
            else
            {
                // You might want to throw an exception or return a default texture
                throw new Exception($"Texture '{assetName}' not found in the AssetManager.");
            }
        }
    }
}

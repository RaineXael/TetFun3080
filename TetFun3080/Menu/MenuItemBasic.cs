using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;
using static System.Net.Mime.MediaTypeNames;

namespace TetFun3080.Menu
{
    internal class MenuItemBasic : IMenuItem
    {
        SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        string Title { get; set; }

        public MenuItemBasic(string itemText)
        {
            Title = itemText;
            Position = Vector2.Zero; // Default position, can be set later
            Font = AssetManager.GetFont("Fonts/Font1");
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Implement drawing logic here
            spriteBatch.DrawString(Font, Title, Position, Color.White);

        }
        public void Update(GameTime gameTime)
        {
            // Implement update logic here
        }

        public void OnSelect()
        {
            throw new NotImplementedException();
        }
    }

}

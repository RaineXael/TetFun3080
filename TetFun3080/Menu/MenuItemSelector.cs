using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Menu
{
    internal class MenuItemSelector : IMenuItem
    {
        SpriteFont Font { get; set; }
        public Vector2 Position { get; set ; }

        public MenuItemSelector()
        {
            Font = AssetManager.GetFont("Fonts/Font1");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.DrawString(Font, Items[i], textPosition, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void OnSelect()
        {
            throw new NotImplementedException();
        }
    }
}

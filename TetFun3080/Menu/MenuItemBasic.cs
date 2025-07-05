using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TetFun3080.Backend;


namespace TetFun3080.Menu
{
    internal class MenuItemBasic : IMenuItem
    {
        SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        string Title { get; set; }

        private bool selected;

        private TextObject text;

        public MenuItemBasic(string itemText, Vector2 pos)
        {
            Title = itemText;
            Position = pos; 
            Font = AssetManager.GetFont("Fonts/Font1");
            text = new TextObject(Title);
            text.Position = pos / ScreenManager.screenScale;
            
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Implement drawing logic here
            if (selected)
            {
                // Draw a background or highlight for the selected item
                text.Draw(spriteBatch, gameTime, Color.Yellow);
            }
            else
            {
                text.Draw(spriteBatch, gameTime, Color.White);
            }
                

        }
        public void Update(GameTime gameTime)
        {
            // Implement update logic here
        }

        public void OnSelect()
        {
            throw new NotImplementedException();
        }

        public void OnSetSelected(bool selected)
        {
            this.selected = selected;
        }
    }

}

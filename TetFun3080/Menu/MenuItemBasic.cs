using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace TetFun3080.Menu
{
    internal class MenuItemBasic : IMenuItem
    {
        SpriteFont Font { get; set; }
        public Vector2 Position { get; set; }
        string Title { get; set; }

        private bool selected;

        public MenuItemBasic(string itemText, Vector2 pos)
        {
            Title = itemText;
            Position = pos; 
            Font = AssetManager.GetFont("Fonts/Font1");
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Implement drawing logic here
            if (selected)
            {
                // Draw a background or highlight for the selected item
                spriteBatch.DrawString(Font, Title, Position, Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(Font, Title, Position, Color.White);
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

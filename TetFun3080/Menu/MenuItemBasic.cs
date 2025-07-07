using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TetFun3080.Backend;


namespace TetFun3080.Menu
{
    internal class MenuItemBasic : IMenuItem
    {
      
        public Vector2 Position { get; set; }
        string Title { get; set; }

        public float Height { get; set; }

        protected bool selected;

        private TextObject text;

        public MenuItemBasic(string itemText, Vector2 pos, int height)
        {
            Title = itemText;
            Position = pos;

            text = new TextObject(Title);
            text.Position = pos / ScreenManager.screenScale;
            Height = height * ScreenManager.screenScale;
        }


        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
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
        public virtual void Update(GameTime gameTime)
        {
            // Implement update logic here
        }

        public void OnSelect()
        {
            throw new NotImplementedException();
        }

        public virtual void OnSetSelected(bool selected)
        {
            this.selected = selected;
            DebugConsole.Log("selector selected");
        }

        public virtual void OnInteracted()
        {
            
        }
    }

}

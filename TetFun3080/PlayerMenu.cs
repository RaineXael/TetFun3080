using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TetFun3080.Backend;
using TetFun3080.Menu;

namespace TetFun3080
{
    internal class PlayerMenu : IEntity
    {
        public Vector2 Position { get; set; }
        public MenuParent menu;
        public PlayerMenu(Vector2 pos)
        {
           Position = pos;
           menu = new MenuParent(Position, new string[] { "Campaign", "Freeplay", "Customize","Settings", "Exit" });
        }

        

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            menu.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }
    }
}

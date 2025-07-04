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
        private UserInput input;
        public PlayerMenu(Vector2 pos, UserInput input)
        {
           Position = pos;
           menu = new MenuParent(Position, input, new string[] { "Campaign", "Freeplay", "Customize","Settings", "Exit" });
           this.input = input;
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

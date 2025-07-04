using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TetFun3080.Backend;
using TetFun3080.Menu;

namespace TetFun3080
{
    internal class PlayerMenu : IEntity
    {
        private Player parent;

        public Vector2 Position { get; set; }
        public MenuParent menu;
        private UserInput input;
        public PlayerMenu(Vector2 pos, UserInput input, Player parent)
        {
            MusicManager.PlayMusic("Audio/Mus/menu");
            this.parent = parent;
           Position = pos;
           menu = new MenuParent(Position, input, new string[] { "Campaign", "Freeplay", "Customize","Settings", "Exit" });
           this.input = input;

            //temp for menu functionality
            menu.PParent = parent;
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

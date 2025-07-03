using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public static class LevelManager
    {
        public static ILevel CurrentLevel { get; private set; }

        public static void SetLevel(ILevel level)
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.OnExit();
            }
            CurrentLevel = level;
            CurrentLevel.OnEnter();
        }

        public static void Update(GameTime gameTime)
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.Draw(spriteBatch, gameTime);
            }
        }

    }
}

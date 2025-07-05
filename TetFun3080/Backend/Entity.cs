using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public class Entity : IEntity
    {
        private Vector2 _position;
        public Vector2 Position { get { return _position * ScreenManager.screenScale; } set { _position = value; } }

        private Vector2 _scale;
        public Vector2 Scale { get { return _scale * ScreenManager.screenScale; } set { _scale = value; } }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

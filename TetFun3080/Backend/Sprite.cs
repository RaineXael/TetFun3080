using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TetFun3080.Backend
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }
  

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Color = Color.White;
            Position = Vector2.Zero;
            Alpha = 1;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color*Alpha);
        }

        public virtual void Update(GameTime gameTime)
        {
            // Default implementation does nothing
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TetFun3080
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            Color = Color.White;
            Position = Vector2.Zero;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color);
        }
   
    }
}

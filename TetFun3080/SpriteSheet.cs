using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TetFun3080
{
    public class SpriteSheet : Sprite
    {
        public int baseSize { get; set; } = 16;

        public SpriteSheet(Texture2D texture) : base(texture)
        {
          
        }

        public void DrawSheet(SpriteBatch spriteBatch, int index)
        { 
            spriteBatch.Draw(Texture, Position, new Rectangle(index*baseSize, 0, baseSize, baseSize), Color);
        }
    }
}

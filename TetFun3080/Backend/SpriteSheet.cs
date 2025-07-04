using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TetFun3080.Backend
{
    public class SpriteSheet : Sprite
    {
        public int baseSize { get; set; } = 16;
        public int spriteIndex = 0;

        public SpriteSheet(Texture2D texture, int baseSize) : base(texture)
        {
            this.baseSize = baseSize;
        }

        public override void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(Texture, Position, new Rectangle(spriteIndex * baseSize, 0, baseSize, baseSize), Color * Alpha);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public class AnimatedSpriteSheet : SpriteSheet
    {
        public EventHandler OnAnimFinish;

        public int animSpeed = 4; //frame delay
        
        public int animTimer;
        int frameCount;

        public AnimatedSpriteSheet(Texture2D texture, int baseSize, int animSpeed) : base(texture, baseSize)
        {
            this.animSpeed = animSpeed;
            animTimer = animSpeed;
            frameCount = texture.Width / baseSize;
        }

        public override void Update(GameTime gameTime)
        {
            if (animTimer > 0)
            {
                animTimer--;
            }
            else
            {
                animTimer = animSpeed;
                spriteIndex++;
                if (spriteIndex >= frameCount)
                {
                    spriteIndex = 0;
                    OnAnimFinish?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}

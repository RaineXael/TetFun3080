using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080
{
    public class Companion :IEntity
    {
        private string baseFolderURL; //kept for unloading

        private AnimatedSpriteSheet idleAnim;
        private AnimatedSpriteSheet waitAnim;
        private AnimatedSpriteSheet lineclearAnim;
        private AnimatedSpriteSheet winAnim;
        private AnimatedSpriteSheet loseAnim;
        private AnimatedSpriteSheet attackAnim;
        private AnimatedSpriteSheet attackgetAnim;
        private AnimatedSpriteSheet direAnim;

        private AnimatedSpriteSheet currentAnimation;
        private Random rng;
        public Companion(string baseFolderURL, Vector2 position)
        {
            rng = new Random();
            timeUntilIdle = rng.Next(120, 260);
            this.baseFolderURL = baseFolderURL;
            AssetManager.LoadTexture(baseFolderURL + "/idle");
            AssetManager.LoadTexture(baseFolderURL + "/wait");
            AssetManager.LoadTexture(baseFolderURL + "/line");
            AssetManager.LoadTexture(baseFolderURL + "/win");
            AssetManager.LoadTexture(baseFolderURL + "/lose");
            AssetManager.LoadTexture(baseFolderURL + "/attack");
            AssetManager.LoadTexture(baseFolderURL + "/attackget");
            AssetManager.LoadTexture(baseFolderURL + "/dire");

            idleAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/idle"), 72, 4);
            waitAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/wait"), 72, 4);
            lineclearAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/line"), 72, 4);
            winAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/win"), 72, 4);
            loseAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/lose"), 72, 4);
            attackAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/attack"), 72, 4);
            attackgetAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/attackget"), 72, 4);
            direAnim = new AnimatedSpriteSheet(AssetManager.GetTexture(baseFolderURL + "/dire"), 72, 4);


            idleAnim.Position = position;
            waitAnim.Position = position;
            lineclearAnim.Position = position;
            winAnim.Position = position;
            loseAnim.Position = position;
            attackAnim.Position = position;
            attackgetAnim.Position = position;
            direAnim.Position = position;

            waitAnim.OnAnimFinish += ResetAnim;

            currentAnimation = idleAnim;
        }
        public void SetDire(object sender, EventArgs e)
        {
            currentAnimation = direAnim;
        }
        public void ResetAnim(object sender, EventArgs e)
        {
            currentAnimation = idleAnim;
        }

  

        public void SwitchAnimation(string name)
        {
            switch(name.ToLower())
            {
                case "idle":
                    currentAnimation = idleAnim;
                    break;
                case "wait":
                    currentAnimation = waitAnim;
                    break;
                case "lineclear":
                    currentAnimation = lineclearAnim;
                    break;
                case "win":
                    currentAnimation = winAnim;
                    break;
                case "lose":
                    currentAnimation = loseAnim;
                    break;
                case "attack":
                    currentAnimation = attackAnim;
                    break;
                case "attackget":
                    currentAnimation = attackgetAnim;
                    break;
                case "dire":
                    currentAnimation = direAnim;
                    break;
                default:
                    DebugConsole.LogError("Invalid animation name: " + name);
                    break;
            }
        }

        int timeUntilIdle;



        public void Update(GameTime gameTime)
        {
           currentAnimation.Update(gameTime);

            if (currentAnimation == idleAnim)
            {
                timeUntilIdle--;
                if (timeUntilIdle <= 0)
                {
                    SwitchAnimation("wait");
                }
            }
            else
            {
                timeUntilIdle = rng.Next(120,260); // Reset the timer when not in idle animation
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentAnimation.Draw(spriteBatch);
        }

        internal void SwitchAnimation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

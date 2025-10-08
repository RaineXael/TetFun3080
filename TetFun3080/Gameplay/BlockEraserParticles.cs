
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    public class BlockEraserParticles : IEntity
    {
        private const int YKillPlane = 10000;

        List<BlockParticle> particles;

        int gravity = 1;

        private SpriteSheet block;

        public BlockEraserParticles(SpriteSheet block) 
        {
            particles = new List<BlockParticle>();
            this.block = block;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (BlockParticle particle in particles)
            {
                particle.Draw(spriteBatch, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            int i = 0;
            while (i < particles.Count) 
            {
                BlockParticle particle = particles[i];
                particle.Update(gameTime);
                if (particle.Position.Y > YKillPlane)
                {
                    particles.RemoveAt(i); 
                                           
                }
                else
                {
                    i++; 
                }
            }

        }
        private Random random = new Random();
        public void SpawnBlock(Vector2 pos, int color)
        {
            particles.Add(new BlockParticle(block, pos * ScreenManager.screenScale, random.Next(-15,-5), random.Next(1,3),random.Next(-8, 8), random.Next(1,6)));
            //particles.Add(new BlockParticle(block, pos, random.Next(-10,-5), random.Next(1,3),random.Next(-8, 8), color));
        }


    }

    public class BlockParticle : IEntity
    {
        public int yVel;
        public int xVel;
        public int yGrav;
        SpriteSheet block;

        public Vector2 Position;

       
        public BlockParticle(SpriteSheet sprite, Vector2 pos, int initialYVel, int yGrav, int xVel, int color)
        {
            Position = pos;
            this.xVel = xVel;
            yVel = initialYVel;
            this.yGrav = yGrav;
            block = sprite;
            block.spriteIndex = color;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            block.Position = Position;
            
                
            block.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            Position = Position + new Vector2(xVel,yVel);
            yVel += yGrav;
        }
    }
}

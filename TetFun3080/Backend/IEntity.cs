﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public interface IEntity
    {



        abstract void Update(GameTime gameTime);
        abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}

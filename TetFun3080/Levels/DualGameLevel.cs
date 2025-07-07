using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TetFun3080.Backend;
using TetFun3080.Gameplay;

namespace TetFun3080.Levels
{
    public class DuoGameLevel : SoloGameLevel
    {
        protected Player player2;
        const int offset = 224;
        public override void OnEnter() 
        {
            JSONLoader jsonLoader = new JSONLoader();
            player1Input = new UserInput();
            player2Input = new UserInput(Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Z, Keys.X, Keys.LeftShift);

            mode = jsonLoader.LoadGameModeFromFile("Content/Rulesets/ruleset.json");

            player1 = new Player(player1Input, new Vector2(400-offset, 100));
            player2 = new Player(player2Input, new Vector2(400+offset, 100));



            AssetManager.LoadEffect("Shaders/Wave");
            _distortionEffect = AssetManager.GetEffect("Shaders/Wave");
            _tempBG = AssetManager.GetTexture("Sprites/Backgrounds/default");


           
        }   
       
        public override void MainDrawCall(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.MainDrawCall(spriteBatch, gameTime);
            player2.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
             player2.Update(gameTime);
        }
    }
}
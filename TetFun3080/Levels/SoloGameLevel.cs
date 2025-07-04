using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TetFun3080.Backend;

namespace TetFun3080.Levels
{
    internal class SoloGameLevel : ILevel
    {
        protected UserInput player1Input;
        protected UserInput player2Input;

        private Player nya;
        private Player waur;

        Texture2D _tempBG;
        Effect _distortionEffect;

        GameMode mode;

        private List<IEntity> _entities = new List<IEntity>();
        public List<IEntity> entities
        {
            get => _entities;
            set => _entities = value;
        }
        public Vector2 Position { get; set; }

        public void OnEnter()
        {
            JSONLoader jsonLoader = new JSONLoader();
            player1Input = new UserInput();
            player2Input = new UserInput(Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Z, Keys.X, Keys.LeftShift);

            mode = jsonLoader.LoadGameModeFromFile("Content/Rulesets/ruleset.json");

            nya = new Player(player1Input, new Vector2(168, 100));
            waur = new Player(player2Input, new Vector2(630, 100));

            //nya.BeginGameMode(mode);
            //waur.BeginGameMode(mode);

            AssetManager.LoadEffect("Shaders/Wave");
            _distortionEffect = AssetManager.GetEffect("Shaders/Wave");
            _tempBG = AssetManager.GetTexture("Sprites/Backgrounds/default");

            
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void OnPause()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _distortionEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            _distortionEffect.Parameters["Texture"].SetValue(_tempBG);
            Matrix worldViewProjection = Matrix.CreateOrthographicOffCenter(
    0, 960,
    540, 0,
    0, 1);
            _distortionEffect.Parameters["WorldViewProjection"].SetValue(worldViewProjection);
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                _distortionEffect
            );


            spriteBatch.Draw(_tempBG, new Vector2(0, 0), Color.White);

            // End the SpriteBatch for this pass.
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            nya.Draw(spriteBatch, gameTime);
            waur.Draw(spriteBatch, gameTime);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            nya.Update(gameTime);
            //waur.Update(gameTime);
        }
    }
}

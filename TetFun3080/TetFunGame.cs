using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using TetFun3080.Backend;

namespace TetFun3080
{
    public class TetFunGame : Game
    {
        public float UI_SCALE = 2f;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
      

        protected UserInput player1Input;
        protected UserInput player2Input;

        


        Sprite aya;
        public TetFunGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            AssetManager._graphics = _graphics;
            _graphics.PreferredBackBufferWidth = 320 * (int)UI_SCALE;
            _graphics.PreferredBackBufferHeight = 180 * (int)UI_SCALE;

            //do this to uncap framerate
            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;

            Ruleset rule = new Ruleset();
            rule.SaveRulesetToFile("User/" + "DefaultRuleset.xml");
            rule.LoadRulesetFromContent("User/" + "DefaultRuleset.xml");


            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            AssetManager.Content = Content;
            IsMouseVisible = true;

            player1Input = new UserInput();
            player2Input = new UserInput(Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Z, Keys.X, Keys.LeftShift);
        }
        private IRandomizer _randomizer = new SevenBagRandomizer(); // or new SevenBagRandomizer();
        private BoardPlayer nya;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            nya = new BoardPlayer(new Board(),player1Input);
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            

            AssetManager.LoadFont("Fonts/Font1");
            AssetManager.LoadTexture("Sprites/blocks");
            AssetManager.LoadAudio("Audio/GameSounds/tgm/place");
            AssetManager.LoadAudio("Audio/GameSounds/tgm/line");
            AssetManager.LoadAudio("Audio/GameSounds/joel/place");
            AssetManager.LoadAudio("Audio/GameSounds/joel/line");
            // TODO: use this.Content to load your game content here

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            nya.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            // TODO: Add your drawing code here
            nya.Draw(_spriteBatch);
            base.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}

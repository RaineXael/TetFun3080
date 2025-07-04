using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TetFun3080.Backend;
using TetFun3080.Levels;

namespace TetFun3080
{
    public class TetFunGame : Game
    {
        public float UI_SCALE = 1f;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public TetFunGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            AssetManager._graphics = _graphics;
            _graphics.PreferredBackBufferWidth = 960 * (int)UI_SCALE;
            _graphics.PreferredBackBufferHeight = 540 * (int)UI_SCALE;

            //do this to uncap framerate
            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;

            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            AssetManager.Content = Content;
            IsMouseVisible = true;

            GameManager.game = this;
            
        }
 
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

            LevelManager.SetLevel(new SoloGameLevel());

        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetManager.fallbackTexture = AssetManager.Content.Load<Texture2D>("Sprites/fallback");
            AssetManager.fallbackSound = AssetManager.Content.Load<SoundEffect>("Audio/fallback");
            AssetManager.LoadFont("Fonts/Font1");
            AssetManager.LoadTexture("Sprites/one");
            AssetManager.LoadTexture("Sprites/blocks");
            AssetManager.LoadTexture("Consoles/default");
            AssetManager.LoadAudio("Audio/GameSounds/tgm/place");
            AssetManager.LoadAudio("Audio/GameSounds/tgm/line");
            AssetManager.LoadAudio("Audio/GameSounds/joel/place");
            AssetManager.LoadAudio("Audio/GameSounds/joel/line");
            // TODO: use this.Content to load your game content here
            DebugConsole.Font = AssetManager.GetFont("Fonts/Font1");
            DebugConsole.BackgroundTex = AssetManager.GetTexture("Sprites/one");


            AssetManager.LoadTexture("Sprites/Backgrounds/default");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            DebugConsole.Update(gameTime);
            
            LevelManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            LevelManager.Draw(_spriteBatch, gameTime);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            base.Draw(gameTime);
            DebugConsole.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}

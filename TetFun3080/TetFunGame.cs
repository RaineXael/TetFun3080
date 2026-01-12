using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TetFun3080.Backend;
using TetFun3080.Levels;

namespace TetFun3080
{
    public class TetFunGame : Game
    {
        const int BASE_WINDOW_WIDTH = 960;
        const int BASE_WINDOW_HEIGHT = 540;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public TetFunGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            ScreenManager.graphics = _graphics;
            
            ScreenManager.SetResolution(BASE_WINDOW_WIDTH, BASE_WINDOW_HEIGHT, 2f, false);
            AssetManager._graphics = _graphics;
            
            

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
            AssetManager.contentIconFallbackTexture = AssetManager.Content.Load<Texture2D>("Sprites/contentfallback");
            AssetManager.LoadTexture("Sprites/one");
            AssetManager.LoadTexture("Blocks/bracket/blocks");
            AssetManager.LoadTexture("Blocks/default/blocks");
            AssetManager.LoadTexture("Consoles/default");
            AssetManager.LoadTexture("Consoles/sketch");
            AssetManager.LoadTexture("Fonts/font");
            AssetManager.LoadAudio("Audio/GameSounds/tgm/place");
            AssetManager.LoadAudio("Audio/GameSounds/tgm/line");
            AssetManager.LoadAudio("Audio/GameSounds/joel/place");
            AssetManager.LoadAudio("Audio/GameSounds/joel/line");
            // TODO: use this.Content to load your game content here
            
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

            MusicManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            LevelManager.Draw(_spriteBatch, gameTime);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            base.Draw(gameTime);
            DebugConsole.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
        }
    }
}

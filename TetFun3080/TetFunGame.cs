using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TetFun3080.Backend;

namespace TetFun3080
{
    public class TetFunGame : Game
    {
        public float UI_SCALE = 1f;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        protected UserInput player1Input;
        protected UserInput player2Input;

        JSONLoader jsonLoader = new JSONLoader();

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



            jsonLoader.SaveRulesetToJSONFile(new Ruleset(), "User/ruleset.json");


            player1Input = new UserInput();
            player2Input = new UserInput(Keys.Left, Keys.Right, Keys.Down, Keys.Up, Keys.Z, Keys.X, Keys.LeftShift);
        }
        private IRandomizer _randomizer = new SevenBagRandomizer(); // or new SevenBagRandomizer();
        private BoardPlayer nya;
        private BoardPlayer waur;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            nya = new BoardPlayer(new Board(), player1Input, new Vector2(168, 100));
            waur = new BoardPlayer(new Board(), player2Input, new Vector2(630, 100));
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


            _distortionEffect = Content.Load<Effect>("Shaders/Wave");
            AssetManager.LoadTexture("Sprites/Backgrounds/default");
            _tempBG = AssetManager.GetTexture("Sprites/Backgrounds/default");

        }
        Texture2D _tempBG;
        Effect _distortionEffect;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            DebugConsole.Update(gameTime);
            // TODO: Add your update logic here
            nya.Update(gameTime);
            //waur.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _distortionEffect.Parameters["Time"].SetValue((float)gameTime.TotalGameTime.TotalSeconds);
            _distortionEffect.Parameters["Texture"].SetValue(_tempBG);
            Matrix worldViewProjection = Matrix.CreateOrthographicOffCenter(
    0, GraphicsDevice.Viewport.Width,
    GraphicsDevice.Viewport.Height, 0,
    0, 1);
            _distortionEffect.Parameters["WorldViewProjection"].SetValue(worldViewProjection);
            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.LinearClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise,
                _distortionEffect
            );


            _spriteBatch.Draw(_tempBG, new Vector2(0, 0), Color.White);

            // End the SpriteBatch for this pass.
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            // TODO: Add your drawing code here
            nya.Draw(_spriteBatch);
            waur.Draw(_spriteBatch);
            base.Draw(gameTime);
            DebugConsole.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}

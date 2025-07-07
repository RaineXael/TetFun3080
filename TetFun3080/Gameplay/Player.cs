using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080.Gameplay
{
    public class Player : IEntity
    {

        private Companion companion;

        public Vector2 Position { get; set; }
        private UserInput _input;

        private IEntity currentEntity;

        private Sprite console;

        public void BeginGameMode(GameMode mode)
        {
            //temp, use tgm style ruleset for first release
            GameMode temp = new JSONLoader().LoadGameModeFromFile("Content/Rulesets/tgm2.json");
            currentEntity = new PlayerGame(new Board(), _input, Position, temp, this);
            PlayerGame game = (PlayerGame)currentEntity;
            game.OnDire += companion.SetDire;
            game.OnUnDire += companion.ResetAnim;

            //currentEntity = new PlayerGame(new Board(), _input, Position, mode, this);
        }

        public void BeginMenu()
        {
            companion.ResetAnim(this, EventArgs.Empty);
            currentEntity = new PlayerMenu(Position, _input, this);
        }

        public Player(UserInput input, Vector2 spawnPos)
        {
           Position = spawnPos;
            _input = input;
            console = new Sprite(AssetManager.GetTexture("Consoles/sketch"));
            companion = new Companion("Companions/default",new Vector2(Position.X-36-60, Position.Y-36+104));
            currentEntity = new PlayerMenu(Position, _input,this);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            console.Color = new Color(0, 0, 0, 200);
            console.Position = ScreenManager.screenScale * Position - new Vector2(112 - 8, 48 - 16) * ScreenManager.screenScale;
            console.Draw(spriteBatch);


            console.Position = ScreenManager.screenScale * Position - new Vector2(112, 48)* ScreenManager.screenScale;
            console.Color = Color.White;
            console.Draw(spriteBatch);

            

            currentEntity.Draw(spriteBatch, gameTime);
            companion.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            companion.Update(gameTime);
            currentEntity.Update(gameTime);
            
        }
    }
}

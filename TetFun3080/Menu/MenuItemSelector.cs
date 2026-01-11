using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;
using static System.Net.Mime.MediaTypeNames;

namespace TetFun3080.Menu
{
    internal class MenuItemSelector : MenuItemBasic
    {

        List<string> Items = new List<string>() { "a", "b", "c", "d"};

        Sprite icon;

        private int selectedIndex;

        private Vector2 iconOffset = new Vector2(44,24) * ScreenManager.screenScale;

        private UserInput input;

        private TextObject selectedTitle;

        public MenuItemSelector(string itemText, Vector2 pos, int height, UserInput input) :base(itemText, pos, height)
        {
            selectedTitle = new TextObject(Items[selectedIndex]);
            icon = new Sprite(AssetManager.contentIconFallbackTexture);
            this.input = input;
            
        }

        private bool leftPressed = false;
        private bool rightPressed = false;
        private bool confirmPressed = false;



        private void ChangeIndex(int change)
        {
            
            selectedIndex += change;
            if (selectedIndex < 0)
            {
                selectedIndex = Items.Count - 1;
            }
            else if (selectedIndex >= Items.Count)
            {
                selectedIndex = 0;
            }

            selectedTitle.Text = Items[selectedIndex];
            DebugConsole.Log("Selected index is now: " + selectedIndex + ", " + Items[selectedIndex]);
        }

        private void SelectItem()
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            icon.Scale = new Vector2(2, 2);
            icon.Position = Position + iconOffset +  new Vector2(0,16) * ScreenManager.screenScale;
            icon.Draw(spriteBatch);
            icon.Scale = new Vector2(1, 1);
            icon.Position = Position + iconOffset + new Vector2(36, 24) * ScreenManager.screenScale;
            icon.Draw(spriteBatch);
            icon.Position = Position + iconOffset + new Vector2(-20, 24) * ScreenManager.screenScale;
            icon.Draw(spriteBatch);
            selectedTitle.Position = (Position + new Vector2(0, 16) * ScreenManager.screenScale) / ScreenManager.screenScale;
            selectedTitle.Draw(spriteBatch, gameTime);
            base.Draw(spriteBatch, gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (selected)
            {
                input.KeyboardState = Keyboard.GetState();

                if (input.IsLeftDown)
                {
                    if (!leftPressed)
                    {
                        //Action
                        ChangeIndex(-1);
                        leftPressed = true;
                    }
                }
                else
                {
                    leftPressed = false;
                }
                if (input.IsRightDown)
                {
                    if (!rightPressed)
                    {
                        ChangeIndex(1);
                        rightPressed = true;
                    }
                }
                else
                {
                    rightPressed = false;
                }
                if (input.IsRotateClockwiseDown)
                {
                    if (!confirmPressed)
                    {
                        //Action
                        SelectItem();
                        confirmPressed = true;
                    }
                }
                else
                {
                    confirmPressed = false;
                }
            }
        }

        public override void OnInteracted()
        {
           
        }

       
    }
}

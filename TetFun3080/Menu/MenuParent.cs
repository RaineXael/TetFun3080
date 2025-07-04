using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetFun3080.Backend;

namespace TetFun3080.Menu
{
    public class MenuParent : IEntity
    {
        private Vector2 padding = new Vector2(16, 16);
        private int separator = 16;
        private List<IMenuItem> Items { get; set; }

        private Vector2 _position;
        public Vector2 Position { get => _position + padding; set=> _position = value; }
        
        private int selectedIndex = 0; // Track the currently selected index

        private UserInput input;

        bool childFocused = false; // Track if a child menu is focused
        MenuParent? parentMenu;
        MenuParent? childMenu;

        public MenuParent(Vector2 pos, UserInput input, string[] items)
        {
            Position = pos;
            Items = new List<IMenuItem>();
            int i = 0;
            foreach (string itemstring in items)
            {
                this.input = input;
                MenuItemBasic menuitem = new MenuItemBasic(itemstring, Position + new Vector2(0, i * separator));
               
                Items.Add(menuitem); // Assuming MenuItemBasic takes a string in its constructor

                i++;
            }
            Items[selectedIndex].OnSetSelected(true);
        }
        

        


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                Items[i].Draw(spriteBatch, gameTime);
                
               
                
            }
        }

        private bool upPressed = false;
        private bool downPressed = false;
        private bool confirmPressed = false;
        private bool leavePressed = false;

        
        private void ChangeIndex(int change)
        {
            Items[selectedIndex].OnSetSelected(false);

            selectedIndex += change;
            if (selectedIndex < 0)
            {
                selectedIndex = Items.Count - 1;
            }
            else if (selectedIndex >= Items.Count)
            {
                selectedIndex = 0;
            }
            Items[selectedIndex].OnSetSelected(true);
            DebugConsole.LogError("Selected index is now: " + selectedIndex);
        }

        private void SelectItem()
        {
           switch (selectedIndex)
            {
                case 0:
                    // Action for the first item
                    DebugConsole.Log("Campaign");
                    break;
                case 1:
                    // Action for the second item
                    DebugConsole.Log("Freeplay");
                    break;
                case 2:
                    // Action for the second item
                    DebugConsole.Log("Customize");
                    break;
                case 3:
                    // Action for the second item
                    DebugConsole.Log("Settings");
                    break;
                case 4:
                    // Action for the second item
                    DebugConsole.Log("Leave");
                    GameManager.QuitGame();
                    break;
                // Add more cases as needed for other items
                default:
                    DebugConsole.LogError("No action defined for this item.");
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            input.KeyboardState = Keyboard.GetState();

            if (input.IsUpDown)
            {
                if (!upPressed)
                {
                    //Action
                    ChangeIndex(-1);
                    upPressed = true;
                }
            }
            else
            {
                upPressed = false;
            }
            if (input.IsDropDown)
            {
                if (!downPressed)
                {
                    ChangeIndex(1);
                    downPressed = true;
                }
            }
            else
            {
                downPressed = false;
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
            if (input.IsRotateCounterClockwiseDown)
            {
                if (!leavePressed)
                {
                    //Action

                    leavePressed = true;
                }
            }
            else
            {
                leavePressed = false;
            }



        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private List<IMenuItem> Items { get; set; }
        public Vector2 Position { get; set; }
        

        public MenuParent(Vector2 pos, string[] items)
        {
            Items = new List<IMenuItem>();
            int i = 0;
            foreach (string itemstring in items)
            {
                MenuItemBasic menuitem = new MenuItemBasic(itemstring);
                menuitem.Position = Position + new Vector2(0, i * 50); // Adjust the Y position for each item
                Items.Add(menuitem); // Assuming MenuItemBasic takes a string in its constructor


            }
            Position = pos;
        }
        




        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                Items[i].Draw(spriteBatch, gameTime);
                
               
                
            }
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}

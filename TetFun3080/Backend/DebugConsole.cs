using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public static class DebugConsole
    {
        public static SpriteFont Font { get; set; }
        public static Texture2D BackgroundTex;
        private static bool visible;
        static List<string> messages = new List<string>();

        private static bool keyHeld = false;

        public static void Update(GameTime gameTime)
        {
            // Toggle visibility with a key press (e.g., F1)
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                if (!keyHeld)
                {
                    visible = !visible;
                    keyHeld = true; // Set the key as held to prevent toggling multiple times
                }
               
            }
            else
            {
                keyHeld = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                spriteBatch.Draw(BackgroundTex, new Rectangle(0,0, 960, 540), new Color(0.5f, 0.5f, 0.5f, 0.5f));
                for (int i = 0; i < messages.Count; i++)
                {
                    spriteBatch.DrawString(Font, messages[i], new Vector2(10, 10 + i * Font.LineSpacing), Color.White);
                }
            }
        }

        public static void Log(string message)
        {
            messages.Add(message);
            if (messages.Count > 30) // Limit to the last 10 messages
            {
                messages.RemoveAt(0);
            }
        }
    }
}

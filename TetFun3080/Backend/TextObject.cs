using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Net.Mime.MediaTypeNames;


namespace TetFun3080.Backend
{
    public enum TextAlignment
    {
        Left,
        Center,
        Right
    }


    public class TextObject : Entity
    {
        public TextAlignment alignment = TextAlignment.Left;
        public SpriteSheet font;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                textChar = _text.ToCharArray();
            }
        }
        private string _text;
        private char[] textChar = new char[0];

        private int separatorX = 8;
        public TextObject(string value, string fontTexture = "font") {
            Text = value;
            font = new SpriteSheet(AssetManager.GetTexture("Fonts/" + fontTexture), 16);
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < textChar.Length; i++)
            {
                font.Position = Position + new Vector2(separatorX * i * ScreenManager.screenScale,0);
                font.spriteIndex = GetAsciiFromChar(textChar[i]);
                font.Draw(spriteBatch);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Color color)
        {
            font.Color = color;
            Draw(spriteBatch, gameTime);
        }

        private int GetAsciiFromChar(char character)
        {
            return (int)character - 32;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}

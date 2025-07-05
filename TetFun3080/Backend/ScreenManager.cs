using Microsoft.Xna.Framework;


namespace TetFun3080.Backend
{
    public static class ScreenManager
    {
        private static int screenWidth = 960;
        private static int screenHeight = 540;
        public static int screenScale = 2;
        internal static GraphicsDeviceManager graphics;

        public static int ScreenWidth {  get { return screenWidth * screenScale; } set { screenWidth = value; } }
        public static int ScreenHeight { get { return screenHeight * screenScale; } set { screenHeight = value; } }

        public static void SetResolution(int width, int height, int scale = 1, bool fullscreen = false)
        {
            screenWidth = width;
            screenHeight = height;
            screenScale = scale;

            graphics.PreferredBackBufferWidth = ScreenManager.ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenManager.ScreenHeight;

            //do this to uncap framerate
            //_graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;

            graphics.ApplyChanges();
        }
    }
}

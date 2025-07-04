using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    public static class MusicManager
    {
        //Music Manager. Handles fadeouts, midlevel transitions, priority and looping.

        private static SoundEffectInstance currentSongInitial;
        private static SoundEffectInstance currentSongLoop;

        private static SoundEffectInstance currentInstance;
        private static string currentSongPath = "";

        public static void PlayMusic(string path)
        {
            if(currentSongPath != path)
            {
                if (currentInstance != null)
                {
                    currentInstance.Stop();
                }
                currentSongPath = path;
                AssetManager.LoadSong(path);
                Song song = AssetManager.GetSong(path);
                currentSongInitial = song.initial.CreateInstance();
                currentSongLoop = song.loop.CreateInstance();

                currentInstance = currentSongInitial;
                currentInstance.IsLooped = false;

                currentInstance.Play();
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (currentSongInitial != null && currentSongInitial.State == SoundState.Stopped)
            {

                currentInstance = currentSongLoop;
                currentInstance.IsLooped = true;
                currentInstance.Play();
                currentSongInitial = null; // Clear the initial song after it has played


            }
        }
    }
}

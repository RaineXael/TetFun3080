using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TetFun3080.Backend
{
    [Serializable]
    public class Song
    {
        [JsonIgnore]
        public SoundEffect initial;
        [JsonIgnore]
        public SoundEffect loop;

        public String title;
        public String artist;
        public int priority;

        [JsonConstructor]
        public Song(String title, String artist, int priority)
        {
            this.title = title;
            this.artist = artist;
            this.priority = priority;
        }

    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace LoopGame
{
    class Music
    {
        private bool seflg;
        const int SE_MAX = 7, BGM_MAX = 4;
         SoundEffect[] se = new SoundEffect[SE_MAX];
         Song[] bgm = new Song[BGM_MAX];
        public SoundEffect[] Se { get { return se; } }
        public void Init()
        {
            seflg = false;
        }
        public void Load(ContentManager content)
        {
            bgm[0] = content.Load<Song>("Star_of_glitter");
            bgm[1] = content.Load<Song>("Ghost_party");
            bgm[2] = content.Load<Song>("Sparkling meteor shower");
            bgm[3] = content.Load<Song>("star_of_glitter_Music Box");
            se[0] = content.Load<SoundEffect>("pom");
            se[1] = content.Load<SoundEffect>("fall");
            se[2] = content.Load<SoundEffect>("die");
            se[3] = content.Load<SoundEffect>("Ene_die");
            se[4] = content.Load<SoundEffect>("Hanabi");
            se[5] = content.Load<SoundEffect>("skill");
            se[6] = content.Load<SoundEffect>("Enter");
        }
        public void SongPlayer(int song)
        {
            if( MediaPlayer.State!=MediaState.Playing)
            {
                MediaPlayer.Play(bgm[song]);
            }
        }
        public void SongStopper()
        {
            MediaPlayer.Stop();
        }
        public void SePlay(int seNum)
        {
            se[seNum].Play();
        }
        public void OneSePlay(int seNum)
        {
            if(!seflg)
            {
                se[seNum].Play();
                seflg = true;
            }
        }
    }
}

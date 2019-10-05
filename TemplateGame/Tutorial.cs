using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LoopGame
{
    class Tutorial
    {
        const int MAX = 5;
        Texture2D[] tex = new Texture2D[MAX];
        Texture2D texEge; 
        int index;
        bool key, keyPre, flag;

        
        Rectangle helpTutoRec;
        const float HELP_PARSENT = 0.7f; 
        bool openHelopTuto; 
        public bool OpenHelopTuto { get { return openHelopTuto; } set { openHelopTuto = value; } }


        public void Ini(int size) 
        {
            tex[0] = tex[1];
            index = 1;
            helpTutoRec = new Rectangle((int)(size * (1- HELP_PARSENT) / 2), (int)(size * (1 - HELP_PARSENT) / 2), (int)(size * HELP_PARSENT), (int)(size * HELP_PARSENT)); 
        }
        public void Load(ContentManager content)
        {
            tex[1] = content.Load<Texture2D>("01");
            tex[2] = content.Load<Texture2D>("02");
            tex[3] = content.Load<Texture2D>("03");
            tex[4] = content.Load<Texture2D>("04");
            tex[0] = tex[1];
            texEge = content.Load<Texture2D>("optionEge");
        }

        public void Key()
        {
            keyPre = key;
            KeyboardState keystate = Keyboard.GetState();
            key = keystate.IsKeyDown(Keys.Enter);
            if (key && !keyPre) flag = true;
            else flag = false;
        }
        public bool Move(SoundEffect enterSe)
        {
            bool flag = false;
            if (this.flag)
            {
                index++;
                enterSe.Play();
                if (index >= MAX)
                {
                    index = 1;
                    flag = true;
                }
                tex[0] = tex[index];
            }
            return flag;
        }
        public void TutoDraw(SpriteBatch sb)
        {
            sb.Draw(tex[0], new Vector2(0, 0), Color.White);
        }
        public void HelpTutoDraw(SpriteBatch sb) 
        {
            if (OpenHelopTuto)
            {
                sb.Draw(texEge,new Vector2(96,96),Color.White);
                sb.Draw(tex[0], helpTutoRec, Color.White);
            }
        }
    }
}

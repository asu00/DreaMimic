using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LoopGame
{
    class Help
    {
        Texture2D helpKey; //右上のヘルプキー表示
        Vector2 keyPos;
        Texture2D help;// ヘルプ画面
        Vector2 helpPos;

        enum HelpState { CLOSE, OPEN, TUTO };
        HelpState nowHelpState;
        public int NowHelpState => (int)nowHelpState;

        Input input = new Input();
        enum KeysNum { H, Y, N };
        Keys[] key;

        public Help()
        {
            helpPos = new Vector2(97, 97);
            keyPos = new Vector2(590, 20);
            key = new Keys[] { Keys.H, Keys.Y, Keys.N };
            input.Init(key);
        }
        public void Init()
        {
            nowHelpState = HelpState.CLOSE;
        }
        public void Load(ContentManager content)
        {
            helpKey = content.Load<Texture2D>("help");
            help = content.Load<Texture2D>("option");
        }

        public void HelpOpen(Action goTitle, Action helpTuto, SoundEffect se) 
        {
            switch (nowHelpState)
            {
                case HelpState.CLOSE:
                    if (input.InputKey((int)KeysNum.H))
                    {
                        se.Play();
                        nowHelpState = HelpState.OPEN;
                    }
                    break;
                case HelpState.OPEN:
                    if (input.InputKey((int)KeysNum.H))
                    {
                        se.Play();
                        nowHelpState = HelpState.CLOSE;
                    }
                    else if (input.InputKey((int)KeysNum.Y))
                    {
                        se.Play();
                        helpTuto();
                        nowHelpState = HelpState.TUTO;
                    }
                    else if (input.InputKey((int)KeysNum.N))
                    {
                        se.Play();
                        goTitle();
                    }
                    break;
                case HelpState.TUTO:

                    break;
                default:
                    break;
            }
        }
        public void TotuEnd() { nowHelpState = HelpState.OPEN; }

        public void Draw(SpriteBatch sb)
        {
            if (nowHelpState == HelpState.OPEN)
                sb.Draw(help, helpPos, Color.White);
            else if (nowHelpState == HelpState.CLOSE)
                sb.Draw(helpKey, keyPos, Color.White);
        }
    }
}

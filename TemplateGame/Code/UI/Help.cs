using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        SoundEffect se;

        Texture2D helpKey; //右上のヘルプキー表示
        Vector2 keyPos;
        Texture2D help;// ヘルプ画面
        Vector2 helpPos;
        Texture2D keyConf;//キー設定
        Vector2 keyConfPos;

        enum HelpState { CLOSE, OPEN, TUTO, KEY };
        HelpState nowHelpState;
        public int NowHelpState => (int)nowHelpState;

        Input input = new Input();
        enum KeysNum { DC, KC, H, U, Y, J, N }; 
        Keys[] key;

        //キー設定
        const int BUTTON = 2;
        Texture2D[] button = new Texture2D[BUTTON];
        Vector2[] buttonPos = new Vector2[BUTTON];
        readonly Vector2[] buttonPosL = new Vector2[] { new Vector2(163, 291), new Vector2(163, 547) };
        readonly Vector2[] buttonPosR = new Vector2[] { new Vector2(419, 291), new Vector2(420, 547) };

        bool[] buttonF = new bool[2];
        public bool DirDraw => buttonF[(int)KeysNum.DC]; //方向表示
        public bool KeyFix => buttonF[(int)KeysNum.KC]; //方向操作固定

        bool initDirDraw = true; //初期の方向表示をするか***new

        public Help()
        {
            helpPos = new Vector2(97, 97);
            keyConfPos = new Vector2(97, 97);
            keyPos = new Vector2(590, 20);
            key = new Keys[] { Keys.I, Keys.K, Keys.H, Keys.U, Keys.Y, Keys.J, Keys.N };
            input.Init(key);

            //ボタンの修正
            for (int i = 0; i < BUTTON; i++)
            {
                //初期の方向表示をする
                if (i == (int)KeysNum.DC && initDirDraw)
                {
                    buttonF[i] = true;
                    buttonPos[i] = buttonPosR[i];
                }
                else
                {
                    buttonF[i] = false;
                    buttonPos[i] = buttonPosL[i];
                }
            }
        }
        public void Init(SoundEffect se)
        {
            nowHelpState = HelpState.CLOSE;
            this.se = se;
        }
        public void Load(ContentManager content)
        {
            helpKey = content.Load<Texture2D>("help");
            help = content.Load<Texture2D>("option");
            keyConf = content.Load<Texture2D>("keyConfig");
            for (int i = 0; i < BUTTON; i++)
                button[i] = content.Load<Texture2D>("button");
        }

        public void HelpOpen(Action goTitle, Action helpTuto, Action enemyUIChecnge)
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
                    else if (input.InputKey((int)KeysNum.U))
                    {
                        se.Play();
                        nowHelpState = HelpState.KEY;
                    }
                    else if (input.InputKey((int)KeysNum.J))
                    {
                        //切り替え
                    }
                    break;
                case HelpState.TUTO:

                    break;
                case HelpState.KEY:
                    KeyConf();
                    enemyUIChecnge(); //プレイヤーが動かないと変わらないので強制呼び出し
                    break;
                default:
                    break;
            }

        }
        public void TotuEnd() { nowHelpState = HelpState.OPEN; }

        //キー設定
        void KeyConf()
        {
            if (input.InputKey((int)KeysNum.U))
            {
                se.Play();
                nowHelpState = HelpState.OPEN;
            }
            else if (input.InputKey((int)KeysNum.KC))
                ButtonChenge((int)KeysNum.KC);
            else if (input.InputKey((int)KeysNum.DC))
                ButtonChenge((int)KeysNum.DC);
        }
        void ButtonChenge(int bn)
        {
            se.Play();
            if (!buttonF[bn])
            {
                buttonF[bn] = true;
                buttonPos[bn] = buttonPosR[bn];
            }
            else
            {
                buttonF[bn] = false;
                buttonPos[bn] = buttonPosL[bn];
            }
            ButtonAnime(bn);
        }
        void ButtonAnime(int bn)
        {

        }



        public void Draw(SpriteBatch sb)
        {
            switch (nowHelpState)
            {
                case HelpState.OPEN:
                    sb.Draw(help, helpPos, Color.White);
                    break;
                case HelpState.CLOSE:
                    sb.Draw(helpKey, keyPos, Color.White);
                    break;
                case HelpState.KEY:
                    sb.Draw(keyConf, keyConfPos, Color.White);
                    for (int i = 0; i < BUTTON; i++)
                        sb.Draw(button[i], buttonPos[i], Color.White);
                    break;
            }
        }
    }
}

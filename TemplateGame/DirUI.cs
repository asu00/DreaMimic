using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace LoopGame
{
    class DirUI
    {

        Texture2D dirUi;
        Vector2[] pos;
        int[] rot;
        const int SIZE = 192;

        enum KeyNum
        {
            Up = 1, Right, Down, Left,
        }


        public void Init(int all)
        {
            pos = new Vector2[all];
            rot = new int[all];
        }
        public void Load(ContentManager cm)
        {
            dirUi = cm.Load<Texture2D>("dir");
        }

        //プレイヤー
        public void DirChecgeP(bool dirF, int pdn)
        {
            if (!dirF) //オフの時
            {
                rot[0] = 0; //プレイヤー
            }
            else//オンの時
            {
                switch (pdn)
                {
                    case (int)KeyNum.Up:
                        rot[0] = 0;
                        break;
                    case (int)KeyNum.Right:
                        rot[0] = 1;
                        break;
                    case (int)KeyNum.Down:
                        rot[0] = 2;
                        break;
                    case (int)KeyNum.Left:
                        rot[0] = 3;
                        break;
                }
            }
        }

        //エネミー
        public void DirChengeE(int pdn, List<int> Edn, bool dirF, int dirMin, int dirMax, int allDirCount)
        {
            for (int i = 1; i < rot.Length; i++)
            {
                int edn = Edn[i - 1];

                if (!dirF) //オフの時
                {

                    //if (i == 0)
                    //{
                    //    rot[i] = 0; //プレイヤー
                    //    continue;
                    //}

                    switch (pdn)
                    {

                        case (int)KeyNum.Up:
                            rot[i] = edn + 0;
                            break;
                        case (int)KeyNum.Right:
                            rot[i] = edn + 3;
                            break;
                        case (int)KeyNum.Down:
                            rot[i] = edn + 2;
                            break;
                        case (int)KeyNum.Left:
                            rot[i] = edn + 1;
                            break;
                    }

                    if (rot[i] > dirMax) rot[i] -= allDirCount;
                    rot[i]--;
                }
                else//オンの時
                {
                    switch (edn)
                    {
                        case (int)KeyNum.Up:
                            rot[i] = 0;
                            break;
                        case (int)KeyNum.Right:
                            rot[i] = 1;
                            break;
                        case (int)KeyNum.Down:
                            rot[i] = 2;
                            break;
                        case (int)KeyNum.Left:
                            rot[i] = 3;
                            break;
                    }
                }
            }
        }
        public void CenterPos(Vector2 pp, Vector2[] ep, int s)
        {
            pos[0] = new Vector2(pp.X - s, pp.Y - s);
            for (int i = 1; i < pos.Length; i++)
            {
                pos[i] = new Vector2(ep[i - 1].X - s, ep[i - 1].Y - s);
            }
        }

        public void Draw(SpriteBatch sb, bool df,bool[] enemyDead)
        {
            if (!df) return;
            sb.Draw(dirUi, pos[0], new Rectangle(SIZE * rot[0], 0, SIZE, SIZE), Color.White);
            for (int i = 1; i < pos.Length; i++) //敵
            {
                if(!enemyDead[i-1])
                sb.Draw(dirUi, pos[i], new Rectangle(SIZE * rot[i], 0, SIZE, SIZE), Color.White * 0.5f);
            }
        }
    }
}

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
        float[] rot;
        const int SIZE = 192;
        const int C_SIZE = SIZE / 2;

        enum KeyNum
        {
            Up = 1, Right, Down, Left,
        }

        public void Init(int all)
        {
            pos = new Vector2[all];
            rot = new float[all];
        }
        public void Load(ContentManager cm)
        {
            dirUi = cm.Load<Texture2D>("dir");
        }

        public void DirChenge(int pdn, List<int> Edn, bool dirF)
        {
            int dn = 0;
            for (int i = 0; i < rot.Length; i++)
            {
                if (!dirF & i == 0) //オフの時
                {
                    rot[i]= MathHelper.ToRadians(0);
                    continue;
                }

                if (i == 0) dn = pdn; //オンの時
                else dn = Edn[i - 1];

                switch (dn)
                {
                    case (int)KeyNum.Up:
                        rot[i] = MathHelper.ToRadians(0);
                        break;
                    case (int)KeyNum.Right:
                        rot[i] = MathHelper.ToRadians(90);
                        break;
                    case (int)KeyNum.Down:
                        rot[i] = MathHelper.ToRadians(180);
                        break;
                    case (int)KeyNum.Left:
                        rot[i] = MathHelper.ToRadians(270);
                        break;
                }
            }
        }
        public void CenterPos(Vector2 pp, Vector2[] ep, int s)
        {
            pos[0] = new Vector2(pp.X + s / 2, pp.Y + s / 2);
            for (int i = 1; i < pos.Length; i++)
            {
                pos[i] = new Vector2(ep[i - 1].X + s / 2, ep[i - 1].Y + s / 2);
            }
        }

        public void Draw(SpriteBatch sb, bool df)
        {
            if (!df) return;
            sb.Draw(dirUi, pos[0], null, Color.White, rot[0], new Vector2(C_SIZE, C_SIZE), Vector2.One, SpriteEffects.None, 0);
            for (int i = 1; i < pos.Length; i++)
            {
                sb.Draw(dirUi, pos[i], null, Color.White * 0.5f, rot[i], new Vector2(C_SIZE, C_SIZE), Vector2.One, SpriteEffects.None, 0);
            }
        }
    }
}

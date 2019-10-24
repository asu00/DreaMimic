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
        Vector2 pos;
        float rot;
        const int SIZE = 192;
        const int C_SIZE = SIZE/2;

        enum KeyNum
        {
            Up = 1, Right, Down, Left,
        }

        public void Load(ContentManager cm)
        {
            dirUi = cm.Load<Texture2D>("dir");
        }

        public void DirChenge(int dn, bool dirF)
        {
            if (!dirF) return;
            switch (dn)
            {
                case (int)KeyNum.Up:
                    rot = MathHelper.ToRadians(0);
                    break;
                case (int)KeyNum.Right:
                    rot = MathHelper.ToRadians(90);
                    break;
                case (int)KeyNum.Down:
                    rot = MathHelper.ToRadians(180);
                    break;
                case (int)KeyNum.Left:
                    rot = MathHelper.ToRadians(270);
                    break;
            }
        }
        public void CenterPos(Vector2 pp, int ps)
        {
            pos = new Vector2(pp.X + ps/2, pp.Y + ps/2);
        }

        public void Draw(SpriteBatch sb, bool df)
        {
            if (!df) return;
            sb.Draw(dirUi, pos, null, Color.White, rot, new Vector2(C_SIZE,C_SIZE) , Vector2.One, SpriteEffects.None, 0);
        }
    }
}

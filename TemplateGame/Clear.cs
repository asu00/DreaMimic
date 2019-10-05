using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoopGame
{
    class Clear
    {
        Texture2D tex;
        float size;
        const int MAX_SIZE = 64;
        const int S_MAX = 1;
        //花火↓
        System.Random rnd = new System.Random();

        const int MAXFLOWER = 15;
        Texture2D texFlower;
        float[] sizeFlower = new float[MAXFLOWER], alphaFlower = new float[MAXFLOWER];
        const float SF_MIN = 0.1f, SF_PULSE = 0.01f;
        const float A_PULSE =0.02f;

        Vector2[] posOrizin = new Vector2[MAXFLOWER], pos = new Vector2[MAXFLOWER];
        int x, y;
        const int POS_PULSE = 2;
        const int X_MAX_R = 11, Y_MAX_R = -8;
        const int Y_MAX = MAX_SIZE * 5;
        const int DROW_HIRGHT = 96;

        int[] distanceFlower = new int[MAXFLOWER];
        const int D_MIN_R= 2, D_MAX_R= 9;
        const int DIS_FIX = 30;
        public Clear() { Ini(); }
        public void Ini()
        {
            size = 0;
            for (int f = 0; f < MAXFLOWER; f++)
            {
                x = rnd.Next(0, X_MAX_R) * MAX_SIZE;
                y = Y_MAX - (rnd.Next(Y_MAX_R, 0) * MAX_SIZE/2);
                distanceFlower[f] = rnd.Next(D_MIN_R, D_MAX_R) * DIS_FIX; 
                sizeFlower[f] = SF_MIN;
                alphaFlower[f] = 0;
                posOrizin[f] = new Vector2(x, y);
                pos[f] = posOrizin[f];
            }
        }
        public void Load(ContentManager content)
        {
            tex = content.Load<Texture2D>("clear");
            texFlower = content.Load<Texture2D>("fireflower");
        }

        public bool ClerAnime()
        {
            bool flag = false;

            FlowerAnime();
            size += SF_PULSE;
            if (size >= 1)
            {
                size = 1;
                flag = true;
            }
            return flag;
        }
        public void FlowerAnime()
        {
            for (int f = 0; f < MAXFLOWER; f++)
            {
                if ((posOrizin[f].Y - pos[f].Y) <= distanceFlower[f])
                    pos[f].Y -= POS_PULSE;
                else sizeFlower[f] += SF_PULSE;
                if (sizeFlower[f] >= 1)
                {
                    sizeFlower[f] = 1f;
                    alphaFlower[f] -= A_PULSE;
                }
                else alphaFlower[f] = 1;
            }
        }
        public void Draw(SpriteBatch sb, int height, int width)
        {
            for (int f = 0; f < MAXFLOWER; f++)
            {
                sb.Draw(texFlower, pos[f] + new Vector2(texFlower.Width / 2, texFlower.Height), new Rectangle(0, 0, texFlower.Width, texFlower.Height), Color.White * alphaFlower[f], 0.0f, new Vector2(texFlower.Width / 2, texFlower.Height), sizeFlower[f], SpriteEffects.None, 0.0f);
            }
            
            sb.Draw(tex, new Vector2(width / 2 - (tex.Width / 2), height / 2 - DROW_HIRGHT) + new Vector2(tex.Width / 2, tex.Height), new Rectangle(0, 0, tex.Width, tex.Height), Color.White, 0.0f, new Vector2(tex.Width / 2, tex.Height), size, SpriteEffects.None, 0.0f);
        }
    }
}

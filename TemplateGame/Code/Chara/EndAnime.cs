using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace LoopGame
{
    class EndAnime
    {
        enum move { stand, drop }
        Texture2D tex, texEge, texbg,texDrop;
        Vector2 pos, posEge;
        const float MAX_POS_Y = 450;
        int texX, texY;
        int countAnime, index,  sc;
        const int SIZE=64;
        float alpha,alphaDrop;
        const float A_PULSE = 0.01f;
        const int COUNT_MAX = 2;
        int[] count = new int[COUNT_MAX];
        int countT;
        const int FLAG_MAX = 3;
        bool[] flag = new bool[FLAG_MAX];
        const int NEWCOUNT = 30, MAXINDEX = 2,SPEED = 2;
        public EndAnime(int height) { Ini(height); }
        public int Sc { get { return sc; } }
        public void Ini(int height)
        {
            pos = new Vector2(-SIZE, -SIZE);
            posEge = new Vector2(0, (height + (height / 2)));
            countAnime = NEWCOUNT;
            index = 0;
            texX = 0;
            texY = 0;
            for (int b = 0; b < COUNT_MAX; b++) count[b] = 140;
            countT = 40;
            for (int b = 0; b < FLAG_MAX; b++) flag[b] = false;
            sc = 0;
            alpha = 0;
            alphaDrop = 0;
        }
        public void Load(ContentManager content, GraphicsDevice graphics)
        {
            tex = content.Load<Texture2D>("playerED");
            texEge = content.Load<Texture2D>("ege");
            texDrop = content.Load<Texture2D>("drop"); 
            texbg = new Texture2D(graphics, 1, 1);
            texbg.SetData<Color>(new Color[] { Color.White });
        }
        public bool ChangeAnime(float x,float y)
        {
            if(!flag[0])this.pos = new Vector2( x, y);
            count[0]--;
            if (count[0] <= 0) flag[0] = true;
            return flag[0];
        }
        public bool Update()
        {
            bool end = false;
            //落ち始める
            if (flag[0])
            {
                countT--;
                if(countT <= 0) Anime();
            }
            if (flag[1])
            {
                if (pos.Y <= MAX_POS_Y) pos.Y += SPEED; 
                sc += SPEED;
                alphaDrop = 1; 
            }
            //白靄
            if (pos.Y - (posEge.Y - sc) >= 150) flag[2] = true;
            if (flag[2]) alpha += A_PULSE;
            if (alpha >= 1)
            {
                alpha = 1;
                count[1]--;
            }
            if (count[1] <= 0) end = true;
            return end;
        }
        public void Anime()
        {
            if (flag[0])
            {
                countAnime--;
                if (countAnime <= 0)
                {
                    index++;
                    if (texX < index) texX++;
                    countAnime = NEWCOUNT;
                }
                if (index >= MAXINDEX)
                {
                    switch (texY)
                    {
                        case (int)move.stand:
                            texY = (int)move.drop;
                            index = 0;
                            texX = MAXINDEX-1;
                            flag[1] = true;
                            break;
                        case (int)move.drop:
                            index = 0;
                            texX = 0;
                            break;
                    }
                }
            }
        }
        public void Draw(SpriteBatch sb, int height)
        {
            sb.Draw(tex, pos, new Rectangle(SIZE * texX, SIZE * texY, SIZE, SIZE), Color.White);
            sb.Draw(texDrop, pos + new Vector2(0, -SIZE), new Rectangle(SIZE * texX, 0, SIZE, SIZE), Color.White * alphaDrop);
            sb.Draw(texEge, new Vector2(0, (height + (height / 2)) - sc), Color.White);
            sb.Draw(texbg, new Rectangle(0, 0, height, height), Color.White * alpha);
        }
    }
}

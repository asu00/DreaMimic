using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace LoopGame
{
    class TitleAndEnd 
    {

        Texture2D title, titleString;
        Texture2D end;


        const int TEX_MAX = 2;
        Texture2D startTex;
        Texture2D[] endTex = new Texture2D[TEX_MAX];
        float[] alpha = new float[TEX_MAX], alp = new float[TEX_MAX];
        Vector2[] pos = new Vector2[TEX_MAX];
        const float A_MIN = -0.006f;
        const float POS_PULSE = 0.1f;

        public void Init()
        {
            pos[0] = new Vector2(0, 0);
            pos[1] = new Vector2(0, 576);
            for (int i = 0; i < TEX_MAX; i++) alp[i] = A_MIN;
            alpha[0] = 1;
            alpha[1] = 0;
        }

        public void Load(ContentManager content)
        {
            title = content.Load<Texture2D>("start");   
            end = content.Load<Texture2D>("end");
            startTex = content.Load<Texture2D>("startAnime");
            titleString = content.Load<Texture2D>("title");
            endTex[0] = content.Load<Texture2D>("endAnime1");
            endTex[1] = content.Load<Texture2D>("endAnime2");
        }
        public void StartAnime(int height)
        {
            for (int i = 0; i < TEX_MAX; i++)
            {
                pos[i].Y -= POS_PULSE;
                if (pos[i].Y + startTex.Height <= 0) pos[i].Y = height;
            }
        }
        public void EndAnime()
        {
            for (int i = 0; i < TEX_MAX; i++)
            {
                if (alpha[i] < 0 || alpha[i] > 1) alp[i] *= -1;
                alpha[i] += alp[i];
            }
        }
        public void BackGround(SpriteBatch sb,int sc)
        {
            for (int i = 0; i < TEX_MAX; i++) sb.Draw(startTex, new Vector2(pos[i].X,pos[i].Y - sc), Color.White);
        }
        public void StartDrow(SpriteBatch sb,int sc)
        {
            sb.Draw(title, Vector2.Zero, Color.White);
            BackGround(sb,sc);
            sb.Draw(titleString, Vector2.Zero, Color.White);
        }
        public void EndDrow(SpriteBatch sb)
        {
            sb.Draw(end, Vector2.Zero, Color.White);
            for (int i = 0; i < TEX_MAX; i++)
            {
                sb.Draw(endTex[i], new Vector2(0, 320), Color.White * alpha[i]);
            }
        }
    }
}

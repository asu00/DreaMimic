using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoopGame
{
    class Start
    {
        enum Action { drop, hit }
        Key key = new Key();
        Texture2D tex, texEF;
        Texture2D[] texString = new Texture2D[2];
        Vector2 pos, posOrizin, posString;
        const float SCALE = 1.6f;
        const float PS_LEFT_POS = 480;
        const float PS_X_PULSE_STAGE = 10, PS_Y_PULSE_ALL_STAGE = 130, PS_Y_PULSE_STAGE = -10;
        int texX, texY, texYPre;
        int texXEF, alpha;
        int texXST;
        int allStageCount;
        int scroll ;
        const int SIZE = 64, SIZE_ST = SIZE/2;
        int count, countEF;
        bool anime, drop;
        const int NEWCOUNT = 30, NEWCOUNTEF = 30, SP = 6;

        public int Sc { get { return scroll; } }

        public void Initialize(float x, float y)
        {
            alpha = 1;
            pos = new Vector2(x, -SIZE);
            posOrizin = new Vector2(x , y);
            posString = new Vector2(64, 256);
            texX = 0; texY = 0; texYPre = texY; texXEF = 0;
            countEF = NEWCOUNTEF;
            count = NEWCOUNT;
            anime = false; drop = true;
        }
        public void Ini(float x, float y,int stageCount,int nowStageNum)
        {
            texXST = nowStageNum;
            scroll = 0;
            allStageCount = stageCount;
            Initialize(x, y);
        }
        public void ReIni(int height, float x, float y)
        {
            scroll = height;
            Initialize(x, y);
        }
        public void Load(ContentManager content)
        {
            tex = content.Load<Texture2D>("playerST");
            texEF = content.Load<Texture2D>("drop");
            texString[0] = content.Load<Texture2D>("string");
            texString[1] = content.Load<Texture2D>("num");
        }

        public bool Move(int height)
        {
            texYPre = texY;
            if (drop) AnimeDrop();
            if (pos.Y < posOrizin.Y) pos.Y += SP;
            else if (scroll < height)
            {
                texY = (int)Action.drop;
                scroll += SP;
            }
            else
            {
                drop = false;
                texY = (int)Action.hit;
                if (texY != texYPre) texX = 0;
                scroll = height;
                alpha = 0;
                AnimePlayer();
            }
            return anime;
        }
        //アニメ処理↓
        public void AnimeDrop()
        {
            countEF--;
            if (countEF <= 0)
            {
                countEF = NEWCOUNTEF;
                texXEF++;
                texX++;
            }
            if (texXEF >= texEF.Width / SIZE) texXEF = 0;
            if (texX >= tex.Width / SIZE) texX = 0;
        }
        public void AnimePlayer()
        {
            count--;
            if (count <= 0)
            {
                count = NEWCOUNT;
                texX++;
            }
            if (texX >= tex.Width / SIZE) anime = true;
        }
        //文字表記↓
        public void PosChange(int width)
        {
            if (posOrizin.X < width / 2) posString.X = PS_LEFT_POS;
            else posString.X = SIZE * 1;
        }
        public void StringChange(int stage) { texXST = stage; }
        public void Draw(SpriteBatch sb)
        {
            //ステージ表記「ex.stage1」
            sb.Draw(texString[0], new Vector2(posString.X, posString.Y - scroll), Color.White);
            sb.Draw(texString[1], new Vector2(posString.X + PS_X_PULSE_STAGE, posString.Y + texString[1].Height + PS_Y_PULSE_STAGE - scroll), new Rectangle(SIZE_ST * (texXST + 1), 0, SIZE_ST, texString[1].Height), Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0);
            sb.Draw(texString[1], new Vector2(posString.X + PS_Y_PULSE_ALL_STAGE, posString.Y + texString[1].Height + PS_Y_PULSE_STAGE - scroll), new Rectangle(SIZE_ST * allStageCount, 0, SIZE_ST, texString[1].Height), Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0);
            //playerアニメ
            sb.Draw(tex, pos, new Rectangle(SIZE * texX, SIZE * texY, SIZE, SIZE), Color.White);
            sb.Draw(texEF, new Vector2(pos.X, pos.Y - SIZE), new Rectangle(SIZE * texXEF, 0, SIZE, SIZE), Color.White * alpha);
        }
    }
}

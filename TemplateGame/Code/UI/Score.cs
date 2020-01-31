using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LoopGame
{
    class Score
    {
        //表示
        Texture2D scoreT;
        Texture2D scoreCo;
        Color TextColor;
        const int SIZE = 32;
        const int ONE_COUNT = 10;

        Vector2 scoreTPos;
        Vector2 scoreCoPos;
        Vector2 scoreMaxCoPos;

        //スコア
        int count;
        const int MAX = 10;
        int[] time = new int[] { 20, 10, 7, 7, 3, 6, 7, 8 }; 
        int nowTime;
        public int NowTime { get { return nowTime; } }
        public int Count { get { return count; } }
        public void Init(int stage)
        {
            TextColor = Color.White;
            scoreTPos = new Vector2(0, 16);
            scoreCoPos = new Vector2(150, 4);
            scoreMaxCoPos = new Vector2(260, 4);

            count = 0;
            nowTime = time[stage];
        }
        public void Load(ContentManager content)
        {
            scoreT = content.Load<Texture2D>("moveCo");
            scoreCo = content.Load<Texture2D>("num");
        }

        public void Pluse()
        {
            count++;
        }

        public void ColorChenge()
        {
            TextColor = Color.Red;
        }


        public void Draw(SpriteBatch sb)
        {
    
            sb.Draw(scoreT, scoreTPos, TextColor);
            sb.Draw(scoreCo, new Rectangle((int)scoreCoPos.X-SIZE, (int)scoreCoPos.Y, SIZE, SIZE + SIZE), new Rectangle(count / ONE_COUNT * SIZE, 0, SIZE, SIZE + SIZE), TextColor);
            sb.Draw(scoreCo, new Rectangle((int)scoreCoPos.X, (int)scoreCoPos.Y, SIZE, SIZE + SIZE), new Rectangle(count % ONE_COUNT * SIZE, 0, SIZE, SIZE + SIZE), TextColor);
            sb.Draw(scoreCo, new Rectangle((int)scoreMaxCoPos.X - SIZE, (int)scoreMaxCoPos.Y, SIZE, SIZE + SIZE), new Rectangle(nowTime / ONE_COUNT * SIZE, 0, SIZE, SIZE + SIZE), TextColor);
            sb.Draw(scoreCo, new Rectangle((int)scoreMaxCoPos.X, (int)scoreMaxCoPos.Y, SIZE, SIZE + SIZE), new Rectangle(nowTime % ONE_COUNT * SIZE, 0, SIZE, SIZE + SIZE), TextColor);
        }

    }
}


using System.Collections.Generic;

namespace LoopGame { 
    class Animetion
    {
        //ダメージ落下
        List<float> rot, alpha, textureSize;
        public List<float> Rot => rot;
        public List<float> Alpha => alpha;
        public List<float> TextureSize => textureSize;

       public const float rPulse = 2.5f, aPulse = 0.01f, tsPulse = 0.01f;

        public void Init(int Indexs, float rMin, float aMax, float tsMax)
        {
            rot = new List<float>();
            alpha = new List<float>();
            textureSize = new List<float>();

            for (int i = 0; i < Indexs; i++)
            {
                rot.Add(rMin);
                alpha.Add(aMax);
                textureSize.Add(tsMax);
            }
        }

        //ダメージ落下
        public void DeadAni(int index)
        {
            if (alpha[index] < 0) return; //最初から0なら何もせず返す
            rot[index] += rPulse;
            alpha[index] -= aPulse;
            textureSize[index] -= tsPulse;
        }
    }
}

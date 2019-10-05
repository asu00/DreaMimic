using System.Collections.Generic;

namespace LoopGame
{
    class ChipNum
    {
        int noChara;
        int dirMin;
        int dirMax;
        int allDirCount;

        //プレイヤー
        public void Init(int noChara)
        {
            this.noChara = noChara;
        }
        //プレイヤーのいるインデックス番号を返す
        public int PlayerIndex(int[] playerSheet)
        {
            List<int> psList= new List<int>();
            psList.AddRange(playerSheet);
            return psList.FindIndex(x => x != noChara);
        }


        //敵
        public void Init(int noChara, int dirMin, int dirMax, int allDirCount)
        {
            this.noChara = noChara;
            this.dirMin = dirMin;
            this.dirMax = dirMax;
            this.allDirCount = allDirCount;
        }
        //敵がいるインデックス番号を返す
        public List<int> EnemyIndex(int[] enemySheet)
        {
            List<int> enemyIndexs = new List<int>();

            for (int i = 0; i < enemySheet.Length; i++)
            {
                if (enemySheet[i] != noChara)
                {
                    enemyIndexs.Add(i);
                }
            }

            return enemyIndexs;
        }
        //敵の向きを求める
        public int EnemyDir(int beforeDir, int playerDir, int enemyDir)
        {
            int dir = enemyDir;
            int diff = playerDir - beforeDir;
            dir += diff;

            if (dir < dirMin) dir += allDirCount;
            else if (dir > dirMax) dir -= allDirCount;

            return dir;
        }

    }
}

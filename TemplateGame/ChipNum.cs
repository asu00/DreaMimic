using System.Collections.Generic;

namespace LoopGame
{
    class ChipNum
    {
        int noChara;
        int dirMin;
        int dirMax;
        int allDirCount;
        enum KeyNum
        {
            Up = 1, Right, Down, Left,
        }

        //プレイヤー
        public void Init(int noChara)
        {
            this.noChara = noChara;
        }
        //プレイヤーのいるインデックス番号を返す
        public int PlayerIndex(int[] playerSheet)
        {
            List<int> psList = new List<int>();
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
        public int ChengeDir(int beforeDir, int playerDir, int enemyDir)
        {
            int dir = enemyDir;
            int diff = playerDir - beforeDir;
            dir += diff;

            if (dir < dirMin) dir += allDirCount;
            else if (dir > dirMax) dir -= allDirCount;

            return dir;
        }
        //プレイヤーの方向補正
        public int TChengeDir(int beforeDir, int InputDir)
        {
            int dir = -1;
            switch (beforeDir)
            {
                case (int)KeyNum.Up:
                    dir = InputDir;
                    break;
                case (int)KeyNum.Right:
                    dir = InputDir + 1;
                    break;
                case (int)KeyNum.Down:
                    dir = InputDir + 2;
                    break;
                case (int)KeyNum.Left:
                    dir = InputDir + 3;
                    break;
            }

            if (dir < dirMin) dir += allDirCount;
            else if (dir > dirMax) dir -= allDirCount;

            return dir;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LoopGame
{
    class Collition
    {
        int[] noFloor;
        int noChara;


        //プレイヤー
        public void Init(int[] noFloor, int noChara)
        {
            this.noFloor = noFloor;
            this.noChara = noChara;
        }
        public bool Coll(int oneIndex, int[] mapSheet, List<int> opponentIndex)
        {
            bool tach = false;

            foreach(int nf in noFloor)
            if (mapSheet[oneIndex] == nf)  //床抜け
            {
                tach = true;
            }
            else
            {
                    foreach(int i in opponentIndex)
                if(i== oneIndex) //プレイヤーと敵
                { tach = true; }
            }

            return tach;
        }

        //敵
        public void Init(int[] noFloor)
        {
            this.noFloor = noFloor;
        }
        public bool Coll(List<int> indexs,bool[] dead, int[] mapSheet, Action<int> DeadTrue)
        {
            bool tach=false;
            foreach (int nf in noFloor)//敵は床だけ
                for (int i = 0; i < indexs.Count; i++)
            {
                    if (mapSheet[indexs[i]] == nf && !dead[i])
                    {
                        DeadTrue(i);
                         tach= true;
                    }
            }
            return tach;
        }


    }
}

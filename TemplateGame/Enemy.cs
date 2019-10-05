using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LoopGame
{
    class Enemy
    {
        Texture2D enemyChip;
        const int CHIP_SIZE = 64;
        bool[] dead;

        //移動　向き
        ChipNum chipNum = new ChipNum();
        Collition collition = new Collition();
        List<int> enemyIndexs;
        List<int> dirNum;
        Vector2[] pos;
        const int A_D_MOVE = 1;
        Vector2[] drawPos;
        const float D_FIX=5;
        enum EnemyKeyNum
        { Up = 1, Right, Down, Left }
        public List<int> EnemyIndexs => enemyIndexs;
        Action enemyDeadSE;

        //アニメーション
        Animetion deadAnime = new Animetion();
        float[] rot, alpha, textureSize;
        const float R_MIN = 0f, A_MAX = 0.9f, TS_MAX = 1;
        int[] animeNum, count;
        const int NEW_COUNT = 30;

        public void Load(ContentManager content) { enemyChip = content.Load<Texture2D>("Enemy"); }
        public void Init(int[] enemySheet,int sheetSize, Action enemyDeadSE)
        {
            enemyIndexs = new List<int>();
            enemyIndexs = chipNum.EnemyIndex(enemySheet); 
            dirNum =  new List<int>();
            foreach (int e in enemyIndexs) dirNum.Add(enemySheet[e]);

            pos = new Vector2[enemyIndexs.Count];
            dead = new bool[enemyIndexs.Count];
            rot = new float[enemyIndexs.Count];
            alpha = new float[enemyIndexs.Count];
            textureSize = new float[enemyIndexs.Count];
            animeNum = new int[enemyIndexs.Count];
            count = new int[enemyIndexs.Count];
            drawPos = new Vector2[enemyIndexs.Count];

            this.enemyDeadSE = enemyDeadSE;

            for (int i = 0; i < enemyIndexs.Count; i++)
            {
                pos[i]=new Vector2(enemyIndexs[i] % sheetSize * CHIP_SIZE, enemyIndexs[i] / sheetSize * CHIP_SIZE); 
                dead[i] = false;
                rot[i] = R_MIN;
                alpha[i] = A_MAX;  
                textureSize[i] = TS_MAX;
                animeNum[i] = 0;
                count[i] = NEW_COUNT;
            }
            DrawDirChenge();

        }
        public void SetNumber(int[] noFloor, int noChara, int dirMin, int dirMax, int allDirCount)
        {
            chipNum.Init(noChara, dirMin, dirMax, allDirCount);
            collition.Init(noFloor);
            deadAnime.Init(enemyIndexs.Count, R_MIN  , A_MAX , TS_MAX );
        }

        public void Move(int beforeDir, int playerDir, int sheetSize)
        {
            for (int i = 0; i < enemyIndexs.Count; i++)
            {
                if (!dead[i])
                {
                    dirNum[i] = chipNum.EnemyDir(beforeDir, playerDir, dirNum[i]);

                    if (dirNum[i] == (int)EnemyKeyNum.Up)
                    {
                        pos[i] += new Vector2(0, -CHIP_SIZE);
                        enemyIndexs[i] -= sheetSize;  
                    }
                    else if (dirNum[i] == (int)EnemyKeyNum.Right)
                    {
                        pos[i] += new Vector2(CHIP_SIZE, 0);
                        enemyIndexs[i] += A_D_MOVE;
                    }
                    else if (dirNum[i] == (int)EnemyKeyNum.Down)
                    {
                        pos[i] += new Vector2(0, CHIP_SIZE);
                        enemyIndexs[i] += sheetSize;
                    }
                    else if (dirNum[i] == (int)EnemyKeyNum.Left)
                    {
                        pos[i] += new Vector2(-CHIP_SIZE, 0);
                        enemyIndexs[i] -= A_D_MOVE;
                    }
                }
            }
            DrawDirChenge();
        }

        //描写する時は少しずらす
        void DrawDirChenge()
        {

            for (int i = 0; i < dirNum.Count; i++)
            {
                switch (dirNum[i])
                {
                    case (int)EnemyKeyNum.Up:
                        drawPos[i]=new Vector2(pos[i].X, pos[i].Y - D_FIX); 
                        break;
                    case (int)EnemyKeyNum.Right:
                        drawPos[i] = new Vector2(pos[i].X + D_FIX, pos[i].Y);
                        break;
                    case (int)EnemyKeyNum.Down:
                       drawPos[i] = new Vector2(pos[i].X, pos[i].Y + D_FIX);
                        break;
                    case (int)EnemyKeyNum.Left:
                        drawPos[i] = new Vector2(pos[i].X - D_FIX, pos[i].Y);
                        break;
                }
            }
        }
        //当たり判定
        public bool DeadAction(int[] mapSheet, int[] plaerSheet)
        {
            if (collition.Coll(enemyIndexs, dead, mapSheet, DeadTrue))
                enemyDeadSE();

            DeadAnime();
            bool allDead = false;
            allDead = dead.All(x => x == true);
            return allDead;
        }
        void DeadTrue(int tachIndex) { dead[tachIndex] = true;}

        //アニメーション
        void DeadAnime()
        {
            for (int i = 0; i < enemyIndexs.Count; i++)
            {
                if (dead[i])
                {
                    dirNum[i] = (int)EnemyKeyNum.Up;
                    deadAnime.DeadAni(i);
                    rot[i] = deadAnime.Rot[i];
                    alpha[i] = deadAnime.Alpha[i];
                    textureSize[i] = deadAnime.TextureSize[i];
                }
            }
        }

        public void Anime()
        {
            for (int i = 0; i < enemyIndexs.Count; i++)
            {
                count[i]--;
                if (count[i] <= 0)
                {
                    count[i] = NEW_COUNT;
                    animeNum[i]++;
                    if (animeNum[i] > 1) animeNum[i] = 0;
                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < enemyIndexs.Count; i++)
            {
                sb.Draw(enemyChip, new Rectangle((int)drawPos[i].X , (int)drawPos[i].Y  , CHIP_SIZE, CHIP_SIZE), new Rectangle(dirNum[i] * CHIP_SIZE, 0, CHIP_SIZE, CHIP_SIZE), Color.White); ;

            }
        }
        public void Draw(SpriteBatch sb, int dropHeight, int sc)
        {
            for (int i = 0; i < enemyIndexs.Count; i++)
            {
                if (!dead[i])
                {
                    sb.Draw(enemyChip, new Rectangle((int)drawPos[i].X, (int)drawPos[i].Y   + dropHeight - sc, CHIP_SIZE, CHIP_SIZE), new Rectangle(dirNum[i] * CHIP_SIZE, animeNum[i] * CHIP_SIZE, CHIP_SIZE, CHIP_SIZE), Color.White);
                }
                else
                    sb.Draw(enemyChip, new Vector2((int)drawPos[i].X, (int)drawPos[i].Y) + new Vector2(CHIP_SIZE / 2, CHIP_SIZE / 2), new Rectangle(dirNum[i] * CHIP_SIZE, 0, CHIP_SIZE, CHIP_SIZE), Color.White * alpha[i], MathHelper.ToRadians(rot[i]), new Vector2(CHIP_SIZE / 2, CHIP_SIZE / 2), textureSize[i], SpriteEffects.None, 0.0f);
            }
        }
    }
}

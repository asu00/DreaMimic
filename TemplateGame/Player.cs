using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace LoopGame
{
    class Player
    {
        //ステータス
        Texture2D playerChip;
        const int CHIP_SIZE = 64;
        bool collDead;
        bool countDead;
        bool nowGame;
        public bool NowGame { get { return nowGame; } set { nowGame = value; } }
        public int Size => CHIP_SIZE;

        //移動　向き
        ChipNum chipNum = new ChipNum();
        Collition collition = new Collition();
        int playerIndex;
        int dirNum, beforeDir;
        Vector2 pos;
        const int A_D_MOVE = 1;
        bool moveF;

        const int NEW_COUNT = 30;
        int count, animeNum;
        public int DirNum => dirNum;
        public Vector2 Pos => pos;
        public int BeforeDir => beforeDir;
        public bool MoveF => moveF;

        //入力
        Input input = new Input();
        Keys[] keys;
        enum KeyNum
        { Up = 1, Right, Down, Left, GoTitle = 8 }; //何もない場所が0なので

        //アニメーション
        const float R_MIN = 0f, A_MAX = 0.9f, TS_MAX = 1;
        Animetion deadAnime = new Animetion();
        const int PLAYER_COUNT = 1;
        const int PLAYER_INDEX_COUNT = PLAYER_COUNT - 1;
        float rot, alpha, textureSize;

        public Player() { }
        public void Load(ContentManager content) { playerChip = content.Load<Texture2D>("Player"); }
        public void Init(int[] playerSheet, int sheetSize)
        {
            collDead = false;
            countDead = false;
            nowGame = true;
            playerIndex = chipNum.PlayerIndex(playerSheet);
            //移動
            dirNum = playerSheet[playerIndex];
            pos = new Vector2(playerIndex % sheetSize * CHIP_SIZE, playerIndex / sheetSize * CHIP_SIZE);
            moveF = false;

            keys = new Keys[] { Keys.W,  Keys.D, Keys.S ,Keys.A,
                                Keys.Up, Keys.Right, Keys.Down ,Keys.Left,Keys.I };
            input.Init(keys);

            count = NEW_COUNT;
            animeNum = 0;
        }
        public void SetNumber(int[] noFloor, int noChara, int dirMin, int dirMax, int allDirCount)
        {
            chipNum.Init(noChara, dirMin, dirMax, allDirCount);
            collition.Init(noFloor, noChara);
            deadAnime.Init(PLAYER_COUNT, R_MIN, A_MAX, TS_MAX);
        }

        public void Move(int[] playerSheet, int sheetSize, int allDirCount, bool dirFix)
        {
            beforeDir = dirNum;
            moveF = true;

            //移動
            if (!collDead || !countDead)
            {
                moveF = true;
                if (input.InputKey((int)KeyNum.Up - 1) || input.InputKey((int)KeyNum.Up - 1 + allDirCount)) //1からなので+1\
                    dirNum = (int)KeyNum.Up;
                else if (input.InputKey((int)KeyNum.Right - 1) || input.InputKey((int)KeyNum.Right - 1 + allDirCount))
                    dirNum = (int)KeyNum.Right;
                else if (input.InputKey((int)KeyNum.Down - 1) || input.InputKey((int)KeyNum.Down - 1 + allDirCount))
                    dirNum = (int)KeyNum.Down;
                else if (input.InputKey((int)KeyNum.Left - 1) || input.InputKey((int)KeyNum.Left - 1 + allDirCount))
                    dirNum = (int)KeyNum.Left;
                else
                {
                    moveF = false;
                    return;
                }

                //方向補正がonだったら補正
                if (dirFix)
                    dirNum = chipNum.TChengeDir(beforeDir, dirNum);

                //移動
                if (dirNum == (int)KeyNum.Up)
                {
                    pos += new Vector2(0, -CHIP_SIZE);
                    playerIndex -= sheetSize;
                }
                else if (dirNum == (int)KeyNum.Right)
                {
                    pos += new Vector2(CHIP_SIZE, 0);
                    playerIndex += A_D_MOVE;
                }
                else if (dirNum == (int)KeyNum.Down)
                {
                    pos += new Vector2(0, CHIP_SIZE);
                    playerIndex += sheetSize;
                }
                else if (dirNum == (int)KeyNum.Left)
                {
                    pos += new Vector2(-CHIP_SIZE, 0);
                    playerIndex -= A_D_MOVE;
                }

            }
        }

        // 当たり判定
        public bool DeadAction(int[] mapSheet, List<int> enemySheet)
        {
            if (collition.Coll(playerIndex, mapSheet, enemySheet))
            {
                collDead = true;
                DeadAnime();
            }
            return collDead;
        }
        //移動数判定
        public bool CountCheck(int count, int time)
        {
            if (count == time)
            {
                countDead = true;
                DeadAnime();
            }
            return countDead;
        }
        //アニメーション
        void DeadAnime()
        {
            if (!collDead && !countDead) return;
            deadAnime.DeadAni(PLAYER_INDEX_COUNT);
            rot = deadAnime.Rot[PLAYER_INDEX_COUNT];
            alpha = deadAnime.Alpha[PLAYER_INDEX_COUNT];
            textureSize = deadAnime.TextureSize[PLAYER_INDEX_COUNT];
        }
        public void Anime()
        {
            count--;
            if (count <= 0)
            {
                count = NEW_COUNT;
                animeNum++;
                if (animeNum > 1) animeNum = 0;
            }
        }
        public void DeadTex() { dirNum = (int)KeyNum.Up; }

        public void Draw(SpriteBatch sb)
        {
            if (!collDead && !countDead)
                sb.Draw(playerChip, new Rectangle((int)pos.X, (int)pos.Y, CHIP_SIZE, CHIP_SIZE), new Rectangle(dirNum * CHIP_SIZE, animeNum * CHIP_SIZE, CHIP_SIZE, CHIP_SIZE), Color.White);
            else
                sb.Draw(playerChip, new Vector2((int)pos.X, (int)pos.Y) + new Vector2(CHIP_SIZE / 2, CHIP_SIZE / 2), new Rectangle(dirNum * CHIP_SIZE, 0, CHIP_SIZE, CHIP_SIZE), Color.White * alpha, MathHelper.ToRadians(rot), new Vector2(CHIP_SIZE / 2, CHIP_SIZE / 2), textureSize, SpriteEffects.None, 0.0f);

        }
    }
}
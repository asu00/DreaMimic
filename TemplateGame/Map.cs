﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace LoopGame
{
    class Map
    {

        //マップチップ
        Texture2D mapChip;
        enum ColorNum { noColor, color0, color100, color50 }//数字＝色の濃さ
        int count, index, co;
        const int ANIME_COUNT = 10;
        int stageCount;
        public int StageCount => stageCount;

        const int CHIP_SIZE = 64;
        const int SHEET_SIZE = 12;
        const int NO_FLOOR01 = (int)ColorNum.noColor;
        const int NO_FLOOR02 = 4;
        int[] mapSheet01 = new int[]
       {
            0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
            0, 4, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0,
            0, 0, 4, 1, 1, 1, 1, 1, 1, 4, 0, 0,
            0, 0, 0, 4, 4, 4, 4, 4, 4, 0, 0, 0,
       };
        int[] mapSheet02 = new int[]
        {
            0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 4, 1, 0, 0,
            0, 1, 1, 1, 4, 1, 1, 1, 1, 4, 1, 0,
            0, 4, 1, 1, 1, 4, 1, 1, 4, 0, 1, 0,
            0, 1, 1, 1, 1, 1, 4, 1, 1, 0, 1, 0,
            0, 4, 4, 1, 1, 4, 1, 4, 4, 1, 1, 0,
            0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
            0, 4, 1, 1, 1, 1, 1, 1, 1, 4, 0, 0,
            0, 0, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0,
        };
        int[] mapSheet03 = new int[]
        {
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0,
             0, 1, 0, 0, 1, 1, 0, 1, 4, 1, 0, 0,
             0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0,
             0, 1, 1, 1, 1, 4, 1, 1, 1, 4, 1, 0,
             0, 4, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0,
             0, 1, 1, 4, 1, 1, 1, 1, 1, 1, 4, 0,
             0, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 0,
             0, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
             0, 0, 4, 1, 1, 4, 1, 1 ,1, 1, 4, 0,
             0, 0, 0, 1, 1, 1, 1, 1, 1, 4, 0, 0,
             0, 0, 0, 4, 4, 4, 4, 4, 4, 0, 0, 0,
        };
        int[] mapSheet04 = new int[]
        {
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0,
             0, 1, 4, 1, 4, 1, 1, 0, 1, 1, 1, 0,
             0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 4, 0,
             0, 4, 4, 4, 0, 1, 1, 1, 1, 1, 0, 0,
             0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0,
             0, 1, 1, 4, 1, 1, 1, 4, 1, 1, 4, 0,
             0, 4, 4, 1, 1, 1, 1, 0, 1, 1, 1, 0,
             0, 1, 1, 1, 1, 1, 1, 0, 4, 1, 1, 0,
             0, 1, 1, 1, 1, 1, 1, 1 ,1, 4, 4, 0,
             0, 4, 4, 4, 1, 1, 1, 1, 1, 0, 0, 0,
             0, 0, 0, 0, 4, 4, 4, 4, 4, 0, 0, 0,
        };
        int[] mapSheet05 = new int[]
        {
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 0,
             0, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 0,
             0, 1, 1, 1, 1, 1, 4, 1, 1, 1, 4, 0,
             0, 1, 4, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0,
             0, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 0,
             0, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 0,
             0, 1, 1, 4, 1, 0, 4, 4, 1, 1, 4, 0,
             0, 1, 4, 1, 4, 1, 1, 1, 4, 4, 0, 0,
             0, 4, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0,
             0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 0,
        };
        int[] mapSheet06 = new int[]
        {
            0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 4, 1, 0, 0,
            0, 1, 1, 1, 4, 1, 1, 1, 1, 4, 1, 0,
            0, 4, 1, 1, 1, 4, 1, 1, 4, 0, 1, 0,
            0, 1, 1, 1, 1, 1, 4, 1, 1, 0, 1, 0,
            0, 4, 4, 1, 1, 4, 1, 4, 4, 1, 1, 0,
            0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0,
            0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
            0, 4, 1, 1, 1, 1, 1, 1, 1, 4, 0, 0,
            0, 0, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0,
        };
        int[] mapSheet07 = new int[]
        {
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 1, 1, 4, 4, 4, 4, 4, 4, 1, 1, 0,
             0, 1, 4, 1, 1, 1, 1, 1, 1, 4, 1, 0,
             0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0,
             0, 4, 0, 1, 1, 1, 1, 1, 1, 0, 4, 0,
             0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0,
             0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0,
             0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0,
             0, 1, 1, 4, 4, 4, 4, 4 ,4, 1, 1, 0,
             0, 4, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0,
             0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0,

        };
        int[] mapSheet08 = new int[]
        {
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 1, 4, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 1, 1, 1, 4, 1, 1, 1, 4, 0, 0,
             0, 0, 1, 1, 4, 1, 4, 1, 4, 1, 0, 0,
             0, 0, 4, 4, 1, 1, 0, 4, 0, 1, 0, 0,
             0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0,
             0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0,
        };
        int[] mapSheetEnd = new int[SHEET_SIZE * SHEET_SIZE]; const int END_SHEET_COUNT=1;

        int[][] mapSheets; //マップを入れる
        int[] nowMapSheet; //現在のシート

        //プレイヤー
        const int NO_CHARA = 0;
        const int DIR_MIN = 1;
        const int DIR_MAX = 4;
        const int ALL_DIR_COUNT = 4;
        int[] playerSheet01 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet02 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet03 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet04 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet05 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet06 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet07 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] playerSheet08 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        int[][] playerSheets;
        int[] nowPlayerSheets;

        //敵
        int[] enemySheet01 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet02 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
             0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet03 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0,
             0, 0, 3, 0, 1, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet04 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet05 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet06 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0,
             0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet07 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };
        int[] enemySheet08 = new int[]
        {
             0, 0, 0 ,0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0,
             0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        };

        int[][] enemySheets;
        int[] nowEnemySheets;

        //シート
        public int[]  NowMapSheet => nowMapSheet;
        public int[]  NowPlayerSheet => nowPlayerSheets;
        public int[]  NowEnemySheet => nowEnemySheets;
        public int SheetSize => SHEET_SIZE;

        //番号
        public int[] NoFloor => new int[] { NO_FLOOR01, NO_FLOOR02 };
        public int NoChara => NO_CHARA;
        public int DirMin => DIR_MIN;
        public int DirMax => DIR_MAX;
        public int AllDirCount => ALL_DIR_COUNT;

        public Map()
        { //ステージを格納 順番注意 特にマップ
            mapSheets = new int[][] { mapSheet01, mapSheet02, mapSheet03, mapSheet04, mapSheet05, mapSheet06, mapSheet07, mapSheet08, mapSheetEnd };
            playerSheets = new int[][] { playerSheet01, playerSheet02, playerSheet03, playerSheet04, playerSheet05, playerSheet06, playerSheet07, playerSheet08 };
            enemySheets = new int[][] { enemySheet01, enemySheet02, enemySheet03, enemySheet04, enemySheet05, enemySheet06, enemySheet07, enemySheet08};
            for (int i = 0; i < SheetSize*SheetSize; i++) mapSheetEnd[i] = (int)ColorNum.noColor;
            stageCount = mapSheets.Length - END_SHEET_COUNT; //エンディング用を引く
            //stageCount = 2; //ショートカット
        }
        public void Load(ContentManager content)
        {
            mapChip = content.Load<Texture2D>("field");
        }
        public void Init(int stageNum)
        {
            count = ANIME_COUNT;
            co = (int)ColorNum.color100;
            index = 0;

            nowMapSheet= mapSheets[stageNum];
            nowPlayerSheets = playerSheets[stageNum];
            nowEnemySheets = enemySheets[stageNum];
        }

        //Endアニメーション
        public void MapDestroy(bool flag, int stageNum)
        {
            if (flag)
            {
                nowMapSheet = mapSheets[stageNum + 1];
            }
        }

        public void Anime()
        {
            count--;
            if (count <= 0)
            {
                count = ANIME_COUNT;
                for (int y = 0; y < SHEET_SIZE; y++)
                {
                    if (nowMapSheet[y * SHEET_SIZE + index] < (int)ColorNum.color50 + 1 && nowMapSheet[y * SHEET_SIZE + index] > (int)ColorNum.noColor) nowMapSheet[y * SHEET_SIZE + index] = co;
                }
                index++;
                if (index > SHEET_SIZE - 1)
                {
                    index = 0;
                    co++;
                }
                if (co > (int)ColorNum.color50) co = (int)ColorNum.color0;
            }
        }

        public void Draw(SpriteBatch sb, int dropHeight, int sc)
        {
            for (int i = 0; i < SheetSize; i++)
            {
                for (int j = 0; j < SheetSize; j++)
                {
                    int oneLine = i * SheetSize + j;
                    sb.Draw(mapChip, new Rectangle(j * CHIP_SIZE, i * CHIP_SIZE + dropHeight - sc, CHIP_SIZE, CHIP_SIZE), new Rectangle(nowMapSheet[oneLine] * CHIP_SIZE, 0, CHIP_SIZE, CHIP_SIZE), Color.White);

                }
            }
        }
    }
}

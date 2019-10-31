// このファイルで必要なライブラリのnamespaceを指定
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

/// <summary>
/// プロジェクト名がnamespaceとなります
/// </summary>
namespace LoopGame
{
    /// <summary>
    /// ゲームの基盤となるメインのクラス
    /// 親クラスはXNA.FrameworkのGameクラス
    /// </summary>
    public class Game1 : Game
    {
        // フィールド（このクラスの情報を記述）
        private GraphicsDeviceManager graphicsDeviceManager;//グラフィックスデバイスを管理するオブジェクト
        private SpriteBatch spriteBatch;//画像をスクリーン上に描画するためのオブジェクト
        const int SIZE = 768;
        string title = "DreaMimic";

        //シーン
        enum Scene { TITLE, TUTORIAL, PLAY_START, PLAY, END_ANIME, END }
        Scene scene;

        //ステージ
        enum Stage { stage1, stage2, stage3, stage4, stage5, stage6, stage7, stage8 }
        int stage;

        //音
        enum SE { WALK, FALL, PLAYER_DIE, ENEMY_DIE, CLEAR, ENEMYWALK, ENTER }
        enum BGM { TITLT, STAGE, LAST_STAGE, END }

        //クラス
        Start start = new Start();
        Player player = new Player();
        Enemy enemy = new Enemy();
        Map map = new Map();
        Key key = new Key();
        Wait wait = new Wait();
        Score score = new Score();
        Tutorial tu = new Tutorial();
        Clear clear = new Clear();
        EndAnime edA = new EndAnime(SIZE);
        TitleAndEnd titleAndEnd = new TitleAndEnd();
        Music music = new Music();
        Help help = new Help();
        DirUI dirUI = new DirUI();

        /// <summary>
        /// コンストラクタ
        /// （new で実体生成された際、一番最初に一回呼び出される）
        /// </summary>
        public Game1()
        {
            //グラフィックスデバイス管理者の実体生成
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = SIZE;
            graphicsDeviceManager.PreferredBackBufferHeight = SIZE;
            //コンテンツデータ（リソースデータ）のルートフォルダは"Contentに設定
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// 初期化処理（起動時、コンストラクタの後に1度だけ呼ばれる）
        /// </summary>
        protected override void Initialize()
        {
            // この下にロジックを記述
            music.Init();  //変わらない 

            TitleInit(); //初起動はタイトル

            // この上にロジックを記述
            base.Initialize();// 親クラスの初期化処理呼び出し。絶対に消すな！！
        }

        //タイトル画面に行くとき呼ぶ
        void TitleInit()
        {
            stage = (int)Stage.stage1;
            scene = Scene.TITLE;
        }
        //どこからの初期化でも変わらない
        void StageInitBase()
        {
            map.Init((int)stage);
            player.Init(map.NowPlayerSheet, map.SheetSize);
            enemy.Init(map.NowEnemySheet, map.SheetSize, EnemyDeadSE);
            start.Ini(player.Pos.X, player.Pos.Y, map.StageCount, (int)stage);
            clear.Ini();
            edA.Ini(SIZE);
            help.Init(music.Se[(int)SE.ENTER]);
            wait.Init();
            score.Init((int)stage);
            dirUI.Init(enemy.EnemyIndexs.Count + 1);

            player.SetNumber(map.NoFloor, map.NoChara, map.DirMin, map.DirMax, map.AllDirCount);
            enemy.SetNumber(map.NoFloor, map.NoChara, map.DirMin, map.DirMax, map.AllDirCount);

        }
        //新しいステージに行くとき
        void StageIni()
        {
            StageInitBase();
            start.PosChange(SIZE);
            scene = Scene.PLAY_START;
            if (stage == map.StageCount - 1)
                music.SongStopper();
        }
        //ゲームオーバーになったとき
        void ReIni()
        {
            StageInitBase();
            start.ReIni(SIZE, player.Pos.X, player.Pos.Y);
            scene = Scene.PLAY_START;
            music.SePlay((int)SE.FALL);
        }

        /// <summary>
        /// コンテンツデータ（リソースデータ）の読み込み処理
        /// （起動時、１度だけ呼ばれる）
        /// </summary>
        protected override void LoadContent()
        {
            // 画像を描画するために、スプライトバッチオブジェクトの実体生成
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // この下にロジックを記述
            titleAndEnd.Load(Content);
            tu.Load(Content);
            start.Load(Content);
            music.Load(Content);
            score.Load(Content);
            help.Load(Content);
            map.Load(Content);
            player.Load(Content);
            enemy.Load(Content);
            clear.Load(Content);
            edA.Load(Content, GraphicsDevice);
            dirUI.Load(Content);
            // この上にロジックを記述
        }

        /// <summary>
        /// コンテンツの解放処理
        /// （コンテンツ管理者以外で読み込んだコンテンツデータを解放）
        /// </summary>
        protected override void UnloadContent()
        {
            // この下にロジックを記述


            // この上にロジックを記述
        }

        /// <summary>
        /// 更新処理
        /// （1/60秒の１フレーム分の更新内容を記述。音再生はここで行う）
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Update(GameTime gameTime)
        {
            // ゲーム終了処理（ゲームパッドのBackボタンかキーボードのエスケープボタンが押されたら終了）
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                 (Keyboard.GetState().IsKeyDown(Keys.Escape)))
            {
                Exit();
            }
            // この下に更新ロジックを記述
            Window.Title = title;

            titleAndEnd.StartAnime(SIZE);
            switch (scene)
            {
                case Scene.TITLE:
                    UpdateStart();
                    break;
                case Scene.TUTORIAL:
                    UpdateTutorial();
                    break;
                case Scene.PLAY_START:
                    UpdatePlayStart();
                    break;
                case Scene.PLAY:
                    UpdatePlay();
                    break;
                case Scene.END_ANIME:
                    UpdateEndAnime();
                    break;
                case Scene.END:
                    UpdateEnd();
                    break;
            }
            // この上にロジックを記述
            base.Update(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
        //タイトル
        void UpdateStart()
        {
            music.SongPlayer((int)BGM.TITLT);
            tu.Ini(SIZE);
            if (key.PushKey(Keys.Enter))
            {
                scene = Scene.TUTORIAL;
                music.SePlay((int)SE.ENTER);
            }
        }
        //チュートリアル
        void UpdateTutorial()
        {
            tu.Key();
            if (tu.Move(music.Se[(int)SE.ENTER]))
            {
                music.SongStopper();
                music.SePlay((int)SE.FALL);
                StageIni();
                scene = Scene.PLAY_START;
            }
        }
        //ステージ開始前の落下アニメ
        void UpdatePlayStart()
        {
            if (stage < map.StageCount - 1)
                music.SongPlayer((int)BGM.STAGE);
            else
                music.SongPlayer((int)BGM.LAST_STAGE);
            map.Anime();
            if (start.Move(SIZE)) scene = Scene.PLAY;
        }
        //ゲーム
        void UpdatePlay()
        {

            if (stage < map.StageCount - 1)
                music.SongPlayer((int)BGM.STAGE);
            else
                music.SongPlayer((int)BGM.LAST_STAGE);

            map.Anime();
            player.Anime();
            enemy.Anime();
            if (player.NowGame)
            {

                if (wait.PlayerInput) //自分のターン
                {
                    HelpInput();
                    music.Init();
                    if (help.NowHelpState == 0)
                        player.Move(map.NowPlayerSheet, map.SheetSize, map.AllDirCount, help.KeyFix);
                    if (player.MoveF) music.SePlay((int)SE.WALK);

                }
                if (wait.WaitCount(player.MoveF)) //敵のターン
                {
                    enemy.Move(player.BeforeDir, player.DirNum, map.SheetSize);
                    music.SePlay((int)SE.ENEMYWALK);
                    score.Pluse();
                }
            }
            //方向表示
            dirUI.CenterPos(player.Pos, enemy.Pos, player.Size);
            dirUI.DirChenge(player.DirNum, enemy.DirNum, help.KeyFix);

            if (enemy.DeadAction(map.NowMapSheet, map.NowPlayerSheet)) //敵が全部死んだら
            {
                EnemyDead();
            }
            else if (player.DeadAction(map.NowMapSheet, enemy.EnemyIndexs)) //自分が死んだら
            {
                PlayerDead();
            }
            else if (player.CountCheck(score.Count, score.NowTime))//移動回数オーバー
            {
                score.ColorChenge();
                PlayerDead();
            }
        }
        void EnemyDeadSE()
        {
            music.SePlay((int)SE.ENEMY_DIE);
        }

        //ヘルプ
        void HelpInput()
        {
            help.HelpOpen(GoTitle, HelpFlagChenge);
            if (tu.OpenHelopTuto) HelpTuto();
        }
        void HelpFlagChenge()
        {
            tu.Ini(SIZE);
            tu.OpenHelopTuto = true;
        }
        void HelpTuto()
        {
            tu.Key();
            if (tu.Move(music.Se[(int)SE.ENTER]))
            {
                tu.OpenHelopTuto = false;
                help.TotuEnd();
            }
        }
        void GoTitle()
        {
            music.SongStopper();
            TitleInit();
        }

        //し
        void EnemyDead()
        {
            player.NowGame = false;
            music.OneSePlay((int)SE.CLEAR);

            if (clear.ClerAnime() && wait.ClearCount())
            {
                if (stage < map.StageCount - 1)
                {
                    stage += 1;
                    start.StringChange((int)stage);
                    StageIni();
                    music.SePlay((int)SE.FALL);
                }
                else
                {
                    music.SongStopper();
                    scene = Scene.END_ANIME;
                }

            }
        }
        void PlayerDead()
        {
            player.NowGame = false;
            music.OneSePlay((int)SE.PLAYER_DIE);

            if (wait.ReStartCount())
            {
                ReIni();
                scene = Scene.PLAY_START;
            }
        }

        //エンディングアニメ
        void UpdateEndAnime()
        {
            map.MapDestroy(edA.ChangeAnime(player.Pos.X, player.Pos.Y), (int)stage);

            if (edA.Update()) scene = Scene.END;
        }
        //エンディング
        void UpdateEnd()
        {
            music.SongPlayer((int)BGM.END);
            titleAndEnd.EndAnime();
            if (key.PushKey(Keys.Enter))
            {
                music.SongStopper();
                TitleInit();
            }

        }


        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gameTime">現在のゲーム時間を提供するオブジェクト</param>
        protected override void Draw(GameTime gameTime)
        {
            // 画面クリア時の色を設定
            GraphicsDevice.Clear(Color.Black);

            // この下に描画ロジックを記述
            spriteBatch.Begin();
            switch (scene)
            {
                case Scene.TITLE:
                    DrawStart();
                    break;
                case Scene.TUTORIAL:
                    DrawTutorial();
                    break;
                case Scene.PLAY_START:
                    DrawPlayStart();
                    break;
                case Scene.PLAY:
                    DrawPlay();
                    break;
                case Scene.END_ANIME:
                    DrawEndAnime();
                    break;
                case Scene.END:
                    DrawEnd();
                    break;
            }
            spriteBatch.End();

            //この上にロジックを記述
            base.Draw(gameTime); // 親クラスの更新処理呼び出し。絶対に消すな！！
        }
        void DrawStart()
        {
            titleAndEnd.StartDrow(spriteBatch,edA.Sc);
        }
        void DrawTutorial()
        {
            titleAndEnd.BackGround(spriteBatch,edA.Sc);
            tu.TutoDraw(spriteBatch);
        }

        //ゲーム画面共用
        void DraawPlayBase()
        {
            titleAndEnd.BackGround(spriteBatch,edA.Sc);
            map.Draw(spriteBatch, SIZE, start.Sc);
            enemy.Draw(spriteBatch, SIZE, start.Sc);
        }
        void DrawPlayStart()
        {
            DraawPlayBase();
            start.Draw(spriteBatch);
        }
        void DrawPlay()
        {
            DraawPlayBase();
            score.Draw(spriteBatch);
            player.Draw(spriteBatch);
            dirUI.Draw(spriteBatch, help.DirDraw);
            clear.Draw(spriteBatch, SIZE, SIZE);
            help.Draw(spriteBatch);
            tu.HelpTutoDraw(spriteBatch);
        }
        void DrawEndAnime()
        {
            DraawPlayBase();
            edA.Draw(spriteBatch, SIZE);
        }
        void DrawEnd()
        {
            titleAndEnd.EndDrow(spriteBatch);
        }
    }
}
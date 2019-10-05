namespace LoopGame
{
    class Wait
    {
        int turnWaitCount;
        const int MAX_COUNT = 20;
        const int PLAYER_WAIT_PLUSE = 10;
        bool countStart;
        bool playerInput;
        public bool PlayerInput => playerInput;

        int reStartCount, clearCount;
        const int RESTART_WAIT_MAX = 120;
        const int CREAR_WAIT_MAX = 160;

        public void Init()
        {
            countStart = false;
            playerInput = true;
            turnWaitCount = 0;
            reStartCount = 0;
        }

        public bool WaitCount(bool playerMoveF)
        {
            if (playerMoveF) { countStart = true; playerInput = false; }

            bool returnF = false;
            if (!countStart) return returnF;

            turnWaitCount++;
            if (turnWaitCount > MAX_COUNT + MAX_COUNT-10) //敵のターン分待つ
            {
                turnWaitCount = 0;
                playerInput = true; //自分が動ける
                countStart = false;
            }
            else if (turnWaitCount == MAX_COUNT)
            {
                returnF = true; //敵が動ける
            }
            return returnF;
        }

        public bool ReStartCount()
        {
            bool end = false;

            reStartCount++;
            if (reStartCount > RESTART_WAIT_MAX)
            {
                reStartCount = 0;
                end = true;
            }
            return end;
        }
        public bool ClearCount()
        {
            bool end = false;

            clearCount++;
            if (clearCount > CREAR_WAIT_MAX)
            {
                clearCount = 0;
                end = true;
            }
            return end;
        }
    }
}

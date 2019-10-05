using Microsoft.Xna.Framework.Input;

namespace LoopGame
{
    class Key
    {
        bool keyR, keyRPre,keyL,keyLPre,keyU,keyUPre,keyD,keyDPre;
        private bool iskeyPush;
        public bool Enter()
        {
            bool flag = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) flag = true;
            return flag;
        }
        public bool PushKey(Keys key)
        {
            bool pushKey = false;
            if (Keyboard.GetState().IsKeyDown(key)) iskeyPush = true;
            if (Keyboard.GetState().IsKeyUp(key) && iskeyPush)
            {
                pushKey = true;
                iskeyPush = false;
            }
            return pushKey;
        }
        public bool PushKey2(Keys key)
        {
            bool pushKey = false;
            if (Keyboard.GetState().IsKeyDown(key)) pushKey = true;
            else pushKey = false;
            return pushKey;
        }
        public bool KeyR()
        {
            bool key = false;
            keyRPre = keyR;
            KeyboardState keystate2 = Keyboard.GetState();
            keyR = keystate2.IsKeyDown(Keys.Right);
            if (keyR && !keyRPre) key = true;
            return key;
        }
        public bool KeyL()
        {
            bool key = false;
            keyLPre = keyL;
            KeyboardState keystate2 = Keyboard.GetState();
            keyL = keystate2.IsKeyDown(Keys.Left);
            if (keyL && !keyLPre) key = true;
            return key;
        }
        public bool KeyU()
        {
            bool key = false;
            keyUPre = keyU;
            KeyboardState keystate2 = Keyboard.GetState();
            keyU = keystate2.IsKeyDown(Keys.Up);
            if (keyU && !keyUPre) key = true;
            return key;
        }
        public bool KeyD()
        {
            bool key = false;
            keyDPre = keyD;
            KeyboardState keystate2 = Keyboard.GetState();
            keyD = keystate2.IsKeyDown(Keys.Down);
            if (keyD && !keyDPre) key = true;
            return key;
        }
    }
}

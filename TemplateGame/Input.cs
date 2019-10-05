using Microsoft.Xna.Framework.Input;

namespace LoopGame
{
    class Input
    {
        Keys[] key;
        bool[] keyUpF;
        bool[] returnF;

        public void Init(Keys[] key)
        {
            this.key = key;
            keyUpF = new bool[key.Length];
            returnF = new bool[key.Length];
        }

        public bool InputKey(int keyNum)
        {
            returnF[keyNum] = false;

            if (Keyboard.GetState().IsKeyUp(key[keyNum]))
                keyUpF[keyNum] = true;
            else if (Keyboard.GetState().IsKeyDown(key[keyNum]) && keyUpF[keyNum])
            {
                keyUpF[keyNum] = false;
                returnF[keyNum] = true;
            }

            return returnF[keyNum];
        }

      
    }
}

using System;

namespace Data
{
    [Serializable]
    public class State
    {
        public int TreesCounter;
        public int Damage;

        public State()
        {
            TreesCounter = 0;
            Damage = 1;
        }
    }
}
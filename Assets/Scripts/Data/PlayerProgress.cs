using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public State State;

        public PlayerProgress(string bootstrapScene)
        {
            WorldData = new WorldData(bootstrapScene);
            State = new State();
        }
    }
}
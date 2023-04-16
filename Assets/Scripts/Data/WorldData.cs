using System;

namespace Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public int CurrentLevel;
        
        public WorldData(string bootstrapScene)
        {
            PositionOnLevel = new PositionOnLevel(bootstrapScene);
            CurrentLevel = 0;
        }
    }
}
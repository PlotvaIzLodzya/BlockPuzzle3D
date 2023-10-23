namespace Assets.BlockPuzzle.Proggression
{
    public struct PlayerProgressionDependency
    {
        public string ExpGUID;
        public string LevelGUID;
        public float MaxExp;
        public int MaxLevel;

        public PlayerProgressionDependency(string expGUID, string levelGUID, float maxExp, int maxLevel)
        {
            ExpGUID = expGUID;
            LevelGUID = levelGUID;
            MaxExp = maxExp;
            MaxLevel = maxLevel;
        }
    }
}


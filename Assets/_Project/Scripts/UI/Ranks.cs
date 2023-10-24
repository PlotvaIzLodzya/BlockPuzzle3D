using Assets.BlockPuzzle.Localization;

namespace Assets.BlockPuzzle.HUD
{
    public static class Ranks
    {
        public static string GetRank(int level)
        {
            var rank = InGameTexts.Rookie;

            rank = level switch
            {
                1 => InGameTexts.Rookie,
                2 => InGameTexts.Beginner,
                3 => InGameTexts.Experienced,
                4 => InGameTexts.Expert,
                5 => InGameTexts.Professional,
                6 => InGameTexts.BigBrain,
                7 => InGameTexts.MegaMind,
                _ => rank
            };

            return rank;
        }
    }
}


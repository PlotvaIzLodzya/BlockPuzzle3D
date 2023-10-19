using System.Collections.Generic;

namespace Assets.BlockPuzzle.HUD
{
    public struct PuzzleViewDependency
    {
        public IEnumerable<StartPuzzleDependency> LetterPuzzles;
        public IEnumerable<StartPuzzleDependency> SquarePuzzles;
        public IEnumerable<StartPuzzleDependency> Puzzles49;

        public PuzzleViewDependency(IEnumerable<StartPuzzleDependency> letterPuzzles,
            IEnumerable<StartPuzzleDependency> squarePuzzles,
            IEnumerable<StartPuzzleDependency> puzzles49)
        {
            LetterPuzzles = letterPuzzles;
            SquarePuzzles = squarePuzzles;
            Puzzles49 = puzzles49;
        }
    }
}


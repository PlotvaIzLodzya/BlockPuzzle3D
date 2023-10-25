using Assets.BlockPuzzle.Puzzles;
using System;

namespace Assets.BlockPuzzle.HUD
{
    public struct StartPuzzleDependency
    {
        public string PuzzleName { get; private set; }
        public bool IsCompleted { get;private set; }
        public Action LoadPuzzle { get; private set; }

        public Difficulty Difficulty { get; private set; }

        public StartPuzzleDependency(Puzzle puzzle, Action loadPuzzle, Difficulty difficulty)
        {
            PuzzleName = puzzle.Name.TranslatedLine;
            IsCompleted = puzzle.IsCompleted;
            LoadPuzzle = loadPuzzle;
            Difficulty = difficulty;
        }
    }
}


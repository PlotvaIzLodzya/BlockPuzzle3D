using Assets.BlockPuzzle.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    [Serializable]
    public class PuzzleFactory
    {
        [SerializeField] private Puzzle[] _puzzlePrefabs;

        public IEnumerable<Puzzle> GetPuzzles<TPuzzle>() where TPuzzle : Puzzle
        {
            var puzzles = new List<Puzzle>();

            foreach (var puzzle in _puzzlePrefabs)
            {
                if (puzzle is TPuzzle)
                    puzzles.Add(puzzle);
            }

            return puzzles;
        }

        public Puzzle GetRandomPuzzle<TPuzzle> () where TPuzzle : Puzzle
        {
            var puzzles = GetPuzzles<TPuzzle>();

            var index = UnityEngine.Random.Range(0, puzzles.Count());

            return puzzles.ElementAt(index);
        }

        public Puzzle GetPuzzle(int index)
        {
            return _puzzlePrefabs[index];
        }

        public bool TryGetPuzzle(string guid, out Puzzle puzzle)
        {
            puzzle = GetPuzzle(guid);

            return puzzle != null;
        }

        public Puzzle GetPuzzle(string guid)
        {
            return _puzzlePrefabs.First(puzzle => puzzle.GUID.Equals(guid));
        }
    }
}


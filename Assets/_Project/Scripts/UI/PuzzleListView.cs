using Assets.BlockPuzzle.Localization;
using Assets.BlockPuzzle.Puzzles;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class PuzzleListView : Panel
    {
        [SerializeField] private StartPuzzleView _startPuzzleViewPrefab;
        [SerializeField] private PuzzleBlockProgression _progression;
        [SerializeField] private TMP_Text _difficultyView;

        private int _completed;
        private int _total;
        private Difficulty _difficulty;

        public void Construct(IEnumerable<StartPuzzleDependency> dependencies)
        {
            _total = dependencies.Count();
            var completed = 0;

            foreach (var dependency in dependencies)
            {
                var view = Instantiate(_startPuzzleViewPrefab, transform);
                view.Construct(dependency);

                if (dependency.IsCompleted)
                    completed++;

                _difficulty = dependency.Difficulty;
            }

            var difficulty = _difficulty switch
            {
                Difficulty.Easy => InGameTexts.Easy,
                Difficulty.Medium => InGameTexts.Medium,
                Difficulty.Hard => InGameTexts.Hard,
                _ => InGameTexts.Hard,
            };

            _difficultyView.text = $"{difficulty}";
            _completed = completed;

            Show();
        }

        public override void Show()
        {
            base.Show();
            _progression.UpdateInfo(_total, _completed);
        }
    }
}


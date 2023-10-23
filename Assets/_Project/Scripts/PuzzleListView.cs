using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class PuzzleListView : Panel
    {
        [SerializeField] private StartPuzzleView _startPuzzleViewPrefab;
        [SerializeField] private PuzzleBlockProgression _progression;

        private int _completed;
        private int _total;

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
            }

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


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class PuzzleListView : Panel
    {
        [SerializeField] private StartPuzzleView _startPuzzleViewPrefab;

        private GridLayoutGroup _layout;

        public void Construct(IEnumerable<StartPuzzleDependency> startPuzzleViewDependencies)
        {
            _layout = GetComponentInChildren<GridLayoutGroup>();

            foreach (var dependency in startPuzzleViewDependencies)
            {
                var view = Instantiate(_startPuzzleViewPrefab, transform);
                view.Construct(dependency);
            }
        }
    }
}


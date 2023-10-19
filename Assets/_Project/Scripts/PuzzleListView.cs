using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.BlockPuzzle.HUD
{
    public class PuzzleListView : Panel
    {
        [SerializeField] private StartPuzzleView _startPuzzleViewPrefab;
        [SerializeField] private GridLayoutGroup _layout;

        public void Construct(IEnumerable<StartPuzzleViewDependency> startPuzzleViewDependencies)
        {
            foreach (var dependency in startPuzzleViewDependencies)
            {
                var view = Instantiate(_startPuzzleViewPrefab, _layout.transform);
                view.Construct(dependency);
            }
        }
    }
}


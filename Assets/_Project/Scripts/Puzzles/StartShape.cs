using Assets.BlockPuzzle.Dependency;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{
    public class StartShape : MonoBehaviour, IShape
    {
        public bool IsPlaced {get; private set;}

        public void Construct(PuzzleDependency dependency)
        {
            IsPlaced = true;
        }

        public void OnComplete()
        {
            
        }
    }
}
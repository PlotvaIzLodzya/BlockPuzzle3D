using Assets.BlockPuzzle.Dependency;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{
    public class StartShape : MonoBehaviour, IShape
    {
        public bool Placed {get; private set;} = true;

        public void Construct(PuzzleDependency dependency)
        {
            
        }
    }
}
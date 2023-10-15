using Assets.BlockPuzzle.Complition;
using Assets.BlockPuzzle.Dependency;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{
    public class Puzzle : MonoBehaviour
    {
        [SerializeField] private Shape[] _shape;
        [SerializeField] private LevelComplition _levelComplition;

        public int ShapesCount => _shape.Length;

        [ContextMenu(nameof(GetFromDependencieFromChildren))]
        public void GetFromDependencieFromChildren()
        {
            _shape= GetComponentsInChildren<Shape>();
            _levelComplition = GetComponentInChildren<LevelComplition>();
            EditorUtility.SetDirty(gameObject);
        }

        public IComplition Construct(PuzzleDependency puzzleDependency)
        {
            foreach (Shape shape in _shape)
            {
                shape.Construct(puzzleDependency);
            }

            _levelComplition.Construct(_shape.Length);

            return _levelComplition;
        }
    }
}
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
        [SerializeField] private float _angleStep = 45;
        [SerializeField] private float _stepTime = 0.2f;
        [SerializeField] private float _rotationTime = 0.2f;

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
                puzzleDependency.SetStepAngle(_angleStep)
                                .SetStepTime(_stepTime)
                                .SetRotaionTime(_rotationTime);

                shape.Construct(puzzleDependency);
            }

            _levelComplition.Construct(_shape.Length);

            return _levelComplition;
        }
    }
}
using Assets.BlockPuzzle.Complition;
using Assets.BlockPuzzle.Dependency;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{
    public class Puzzle : MonoBehaviour
    {
        [SerializeField] private LevelComplition _levelComplition;
        [SerializeField] private float _angleStep = 45;
        [SerializeField] private float _stepTime = 0.2f;
        [SerializeField] private float _rotationTime = 0.2f;

        private IShape[] _shape;

        public int ShapesCount => _shape.Length;

        public void GetFromDependencieFromChildren()
        {
            _shape= GetComponentsInChildren<IShape>();
            _levelComplition = GetComponentInChildren<LevelComplition>();
        }

        public IComplition Construct(PuzzleDependency puzzleDependency)
        {
            GetFromDependencieFromChildren();

            foreach (IShape shape in _shape)
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
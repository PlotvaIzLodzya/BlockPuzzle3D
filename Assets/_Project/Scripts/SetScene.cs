using Assets.BlockPuzzle.Complition;
using Assets.BlockPuzzle.Puzzles;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class SetScene: MonoBehaviour
    {
        private const int GrabLayer = 7;
        private const int ProjectionLayer = 10;
        private const int LevelComplitionLayer = 6;

        public void Set(PuzzleMaterials puzzleMaterials)
        {
            var shapes = FindObjectsOfType<Shape>();
            foreach (var shape in shapes)
            {
                shape.GetComponent<MeshRenderer>().sharedMaterial = puzzleMaterials.ShapeMaterial;
                SetLayer(shape.transform, GrabLayer);
                SetLayer(shape.GetComponentInChildren<Projection>().transform, ProjectionLayer);
            }

            var levelComplition = FindObjectsOfType<LevelComplition>();

            foreach (var complition in levelComplition)
            {
                SetLayer(complition.transform, LevelComplitionLayer);
            }

            var frames = FindObjectsOfType<Frame>();
            foreach (var frame in frames)
            {
                frame.Construct();
                frame.GetComponent<MeshRenderer>().sharedMaterial = puzzleMaterials.FrameMaterial;
            }

            var puzzles = FindObjectsOfType<Puzzle>();

            foreach (var puzzle in puzzles)
            {
                puzzle.GetFromDependencieFromChildren();
#if(UNITY_EDITOR)
                UnityEditor.EditorUtility.SetDirty(puzzle.gameObject);
#endif
            }
        }

        private void SetLayer(Transform transform, int layer)
        {
            transform.gameObject.layer = layer;
            foreach (var children in transform.GetComponentsInChildren<Transform>())
            {
                children.gameObject.layer = layer;
            }
        }
    }
}


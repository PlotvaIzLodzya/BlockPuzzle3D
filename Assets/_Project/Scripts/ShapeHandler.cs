using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class ShapeHandler : MonoBehaviour
    {
        [SerializeField] private Shape[] _shape;

        private void Awake()
        {
            foreach (Shape shape in _shape)
            {
                shape.Construct();
            }
        }
    }
}
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
                var x = UnityEngine.Random.Range(-3 , 3);
                var z = UnityEngine.Random.Range(-3 , 3);
                var position = new Vector3(x, 0, z);
                shape.transform.position += position;
                shape.Construct();
            }
        }
    }
}
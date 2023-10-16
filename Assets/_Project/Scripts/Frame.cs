using Unity.VisualScripting;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Frame : MonoBehaviour
    {
        public void Construct()
        {
            if(GetComponent<MeshCollider>() == null)
                transform.AddComponent<MeshCollider>();
        }
    }
}
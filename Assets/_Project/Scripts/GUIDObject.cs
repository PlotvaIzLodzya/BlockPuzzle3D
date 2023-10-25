using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class GUIDObject : MonoBehaviour
    {
#if (UNITY_EDITOR)
        [ReadOnly]
#endif
        [SerializeField] public string GUID;

        [ContextMenu(nameof(Generate))]
        public void Generate()
        {
#if(UNITY_EDITOR)
            GUID = Guid.NewGuid().ToString();   
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }
}


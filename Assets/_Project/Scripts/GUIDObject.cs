using System;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class GUIDObject : MonoBehaviour
    {
        [field: SerializeField,ReadOnly] public string GUID { get; private set; }

        [ContextMenu(nameof(Generate))]
        public void Generate()
        {
            GUID = Guid.NewGuid().ToString();   
            EditorUtility.SetDirty(this);
        }
    }
}


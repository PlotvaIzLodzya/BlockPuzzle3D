using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    [Serializable]
    public class PuzzleMaterials
    {
        [field: SerializeField] public Material ShapeMaterial { get; private set;}
        [field: SerializeField] public Material FrameMaterial { get; private set;}
    }
}


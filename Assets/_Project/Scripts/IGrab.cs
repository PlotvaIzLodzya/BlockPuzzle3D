using UnityEngine;

namespace Assets.BlockPuzzle.Controll
{
    interface IGrab
    {
        public Transform transform { get; }
        public bool CanPlace();
        public void Grab();
        public void Place();
        public void Return();
        public void SetPosition(Vector3 position);
    }
}
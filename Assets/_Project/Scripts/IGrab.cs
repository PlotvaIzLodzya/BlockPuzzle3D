using UnityEngine;

namespace Assets.BlockPuzzle.Controll
{
    public interface IGrabable
    {
        public bool CanBeGrabbed { get; }
        public Transform transform { get; }
        public bool CanPlace();
        public bool IsPlaced { get; }
        public void Grab();
        public void Place();
        public void Return();

        public void Flip();
        public void RotateToLeft();
        public void RotateToRight();
        public void SetPosition(Vector3 position);
    }
}
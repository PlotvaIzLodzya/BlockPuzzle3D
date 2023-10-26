using Assets.BlockPuzzle.Controll;
using UnityEngine;

namespace Assets.BlockPuzzle.HUD
{
    public class ControllsHUD : Panel
    {
        [SerializeField] private GameButton _rotateLeft;
        [SerializeField] private GameButton _rotateRight;
        [SerializeField] private GameButton _rotateFlip;
        [SerializeField] private GameButton _rotatePlace;

        private IGrab _grab;

        public void Construct(IGrab grab)
        {
            _grab = grab;
            _rotateLeft.Button.onClick.AddListener(RotateLeft);
            _rotateRight.Button.onClick.AddListener(RotateRight);
            _rotateFlip.Button.onClick.AddListener(Flip);
            _rotatePlace.Button.onClick.AddListener(Place);
        }

        public void RotateLeft()
        {
            _grab.CurrentGrab?.RotateToLeft();
        }

        public void RotateRight()
        {
            _grab.CurrentGrab?.RotateToRight();
        }

        public void Place()
        {
            _grab.Place();
        }

        public void Flip()
        {
            _grab.CurrentGrab?.Flip();
        }
    }
}


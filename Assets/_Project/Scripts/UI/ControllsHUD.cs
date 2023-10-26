using Assets.BlockPuzzle.Controll;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
        private RectTransform _rect;
        private List<GameButton> _buttons;

        public void Construct(IGrab grab)
        {
            _buttons = new List<GameButton>()
            {
                _rotatePlace,
                _rotateFlip,
                _rotateLeft,
                _rotateRight
            };
            _rect = GetComponent<RectTransform>();  
            _grab = grab;
            _grab.ShowControl += Show;
            _grab.HideControl += Hide;
            _rotateLeft.Button.onClick.AddListener(RotateLeft);
            _rotateRight.Button.onClick.AddListener(RotateRight);
            _rotateFlip.Button.onClick.AddListener(Flip);
            _rotatePlace.Button.onClick.AddListener(Place);
        }

        private void OnDestroy()
        {
            _grab.ShowControl -= Show;
            _grab.HideControl -= Hide;
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

        public override void Show()
        {
            base.Show();
            var position = Camera.main.WorldToScreenPoint(_grab.CurrentGrab.transform.position);
            _rect.position = position;


            foreach (var button in _buttons)
            {
                Animate(button.RectTransform);
            }
        }
        
        private void Animate(RectTransform rect)
        {
            var initialPos = rect.localPosition;

            rect.localPosition = Vector3.zero;

            rect.DOLocalMove(initialPos, duration : 0.2f);
        }
    }
}


using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.BlockPuzzle.Controll
{

    public interface IGrab
    {
        public IGrabable CurrentGrab { get; }
        public void Place();

        public event Action HideControl;
        public event Action ShowControl;
    }

    public class Grab: MonoBehaviour, IGrab
    {
        private LayerMask _groundMask;
        private LayerMask _grabMask;

        public event Action HideControl;
        public event Action ShowControl;

        public IGrabable CurrentGrab { get; private set; }
        
        public void Construct(Masks masks)
        {
            _grabMask = masks.Grab;
            _groundMask = masks.Ground;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if(CurrentGrab != null && Input.GetMouseButtonUp(0))
            {
                ShowControl?.Invoke();
            }

            if (CurrentGrab != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 50, _grabMask) && hit.rigidbody.TryGetComponent(out IGrabable grabableObject) && grabableObject.CanBeGrabbed && grabableObject!=CurrentGrab)
                {
                    if (CurrentGrab.IsPlaced == false)
                        CurrentGrab.Return();

                    GrabObject(grabableObject);
                }

                if (Physics.Raycast(ray, out RaycastHit groundHit, 100, _groundMask) && Input.GetMouseButton(0))
                {
                    var position = groundHit.point;
                    CurrentGrab.SetPosition(position);
                    HideControl.Invoke();
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    CurrentGrab.Return();
                    CurrentGrab = null;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Place();
                }
            }

            if (Input.GetMouseButtonDown(0) && CurrentGrab == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 50, _grabMask) && hit.rigidbody.TryGetComponent(out IGrabable grabableObject) && grabableObject.CanBeGrabbed)
                {
                    GrabObject(grabableObject);
                }
            }
        }

        public void Place()
        {
            if (CurrentGrab == null)
                return;

            if (CurrentGrab.CanPlace())
                CurrentGrab.Place();
            else
                CurrentGrab.Return();

            HideControl?.Invoke();

            CurrentGrab = null;
        }

        public void Flip()
        {
            CurrentGrab?.Flip();
        }

        public void RotateToRight()
        {
            CurrentGrab?.RotateToRight();
        }

        public void RotateToLeft() 
        {
            CurrentGrab?.RotateToLeft();
        }

        private void GrabObject(IGrabable grab)
        {
            CurrentGrab = grab;
            CurrentGrab.Grab();
            HideControl?.Invoke();
        }
    }
}


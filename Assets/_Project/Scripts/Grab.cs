using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.BlockPuzzle.Controll
{
    public class Grab: MonoBehaviour
    {
        private LayerMask _groundMask;
        private LayerMask _grabMask;

        private IGrabable _currentGrab;
        
        public void Construct(Masks masks)
        {
            _grabMask = masks.Grab;
            _groundMask = masks.Ground;
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("asdasd");
                return;
            }


            if (_currentGrab != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit, 50, _grabMask) && hit.rigidbody.TryGetComponent(out IGrabable grabableObject) && grabableObject.CanBeGrabbed && grabableObject!=_currentGrab)
                {
                    if (_currentGrab.IsPlaced == false)
                        _currentGrab.Return();

                    GrabObject(grabableObject);
                }

                if (Physics.Raycast(ray, out RaycastHit groundHit, 100, _groundMask) && Input.GetMouseButton(0))
                {
                    var position = groundHit.point;
                    _currentGrab.SetPosition(position);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _currentGrab.Return();
                    _currentGrab = null;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Place();
                }
            }

            if (Input.GetMouseButtonDown(0) && _currentGrab == null)
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
            if (_currentGrab == null)
                return;

            if (_currentGrab.CanPlace())
                _currentGrab.Place();
            else
                _currentGrab.Return();

            _currentGrab = null;
        }

        public void Flip()
        {
            _currentGrab?.Flip();
        }

        public void RotateToRight()
        {
            _currentGrab?.RotateToRight();
        }

        public void RotateToLeft() 
        {
            _currentGrab?.RotateToLeft();
        }

        private void GrabObject(IGrabable grab)
        {
            _currentGrab = grab;
            _currentGrab.Grab();
        }
    }
}


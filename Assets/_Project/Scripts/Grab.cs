using UnityEngine;

namespace Assets.BlockPuzzle.Controll
{
    public class Grab: MonoBehaviour
    {
        private LayerMask _groundMask;
        private LayerMask _grabMask;

        private IGrab _currentGrab;
        
        public void Construct(Masks masks)
        {
            _grabMask = masks.Grab;
            _groundMask = masks.Ground;
        }

        private void Update()
        {
            if (_currentGrab != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, _groundMask))
                {
                    var position = hit.point;
                    _currentGrab.SetPosition(position);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _currentGrab.Return();
                    _currentGrab = null;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if (_currentGrab.CanPlace())
                        _currentGrab.Place();
                    else
                        _currentGrab.Return();

                    _currentGrab = null;
                }
            }

            if (Input.GetMouseButtonDown(0) && _currentGrab == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 50, _grabMask) && hit.rigidbody.TryGetComponent(out IGrab grab) && grab.CanBeGrabbed)
                {
                    _currentGrab = grab;
                    _currentGrab.Grab();
                }
            }
        }
    }
}


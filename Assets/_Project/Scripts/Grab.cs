using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.BlockPuzzle.Controll
{
    public class Grab: MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;

        private IGrab _currentGrab;

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
                    {
                        _currentGrab.Place();
                        _currentGrab = null;
                    }
                    else
                    {
                        _currentGrab.Return();
                        _currentGrab = null;
                    }

                }
            }

            if (Input.GetMouseButtonDown(0) && _currentGrab == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.rigidbody.TryGetComponent(out IGrab grab))
                {
                    _currentGrab = grab;
                    _currentGrab.Grab();
                }
            }


        }
    }
}


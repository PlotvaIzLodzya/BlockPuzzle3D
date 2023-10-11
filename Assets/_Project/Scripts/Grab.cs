using UnityEngine;

namespace Assets.BlockPuzzle.Controll
{
    public class Grab: MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;

        private IGrab _currentGrab;
        private Vector3 _currentOffset;

        private void Update()
        {
            if (_currentGrab != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100, _groundMask))
                {
                    var position = hit.point + _currentOffset;
                    _currentGrab.SetPosition(position);
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _currentGrab.Release();
                    _currentGrab = null;
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _currentGrab.Place();
                    _currentGrab = null;
                }
            }

            if (Input.GetMouseButtonDown(0) && _currentGrab == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.rigidbody.TryGetComponent(out IGrab grab))
                {
                    if (Physics.Raycast(ray, out RaycastHit groundHit, 100, _groundMask))
                        _currentOffset = grab.transform.position - groundHit.point;

                    _currentGrab = grab;
                    _currentGrab.Grab();
                }
            }


        }
    }
}


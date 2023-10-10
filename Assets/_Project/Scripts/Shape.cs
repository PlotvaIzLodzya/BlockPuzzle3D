using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle
{

    public class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeMovement _movement;
        [SerializeField] private ShapeRotation _rotation;

        private bool _choosen;
        private bool _placed;
        private Vector3 _defaultPosition;

        public void Construct()
        {
            _movement.Construct(transform);
            _rotation.Construct(transform);
            _defaultPosition = transform.position;
        }

        [ContextMenu(nameof(DarkMagic))]
        private void DarkMagic()
        {

            var rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            var view = new GameObject("ShapeRender");
            view.transform.SetParent(transform, false);
            view.transform.localScale = Vector3.one*0.95f;
            var renderer = view.AddComponent<MeshRenderer>();
            var filter = view.AddComponent<MeshFilter>();
            var currentRender = GetComponent<MeshRenderer>();
            var currentFilter = GetComponent<MeshFilter>();
            renderer.sharedMaterial = currentRender.sharedMaterial;
            filter.sharedMesh = currentFilter.sharedMesh;

            var collider = view.AddComponent<MeshCollider>();
            collider.convex = true;
            collider.isTrigger = true;

            DestroyImmediate(currentRender);
            DestroyImmediate(currentFilter);


            EditorUtility.SetDirty(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_choosen == false)
                return;

            _movement.Return();
            _rotation.Return();
        }

        private void OnMouseDown()
        {
            _choosen = true;
            _placed = false;
            _movement.Move(Vector3.up);
        }

        public void Update()
        {
            if( _choosen == false )
                return;

            if (_movement.IsMoving || _rotation.IsRotating)
                return;

            if(Input.GetKeyDown(KeyCode.W))
                _movement.Move(Vector3.forward);
            if(Input.GetKeyDown(KeyCode.S))
                _movement.Move(Vector3.back);
            if(Input.GetKeyDown(KeyCode.A))
                _movement.Move(Vector3.left);
            if(Input.GetKeyDown(KeyCode.D))
                _movement.Move(Vector3.right);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _movement.Move(Vector3.down);
                _placed = true;
                _choosen = false;
            }

            if(Input.GetKeyDown(KeyCode.Q))
                _rotation.Rotate(Vector3.down);
            if(Input.GetKeyDown(KeyCode.E))
                _rotation.Rotate(Vector3.up);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _choosen = false;

                if(_placed == false)
                    _movement.MoveTo(_defaultPosition);
            }
        }
    }

    [Serializable]
    public class ShapeRotation
    {
        [SerializeField] private AnimationCurve _rotatuibEase;

        private Transform _transform;
        private Coroutine _rotatingCoroutine;
        private Quaternion _lastRotation;

        public bool IsRotating { get; private set; }

        public void Construct(Transform transform)
        {
            _transform = transform;
            _lastRotation = transform.rotation;
        }

        public void Rotate(Vector3 direction)
        {
            var angle = direction * 45f;
            var position = _transform.rotation * Quaternion.Euler(angle);
            Stop();
            _rotatingCoroutine = Game.Instance.StartCoroutine(Rotating(position, 0.2f));
        }

        public void Stop()
        {
            if (_rotatingCoroutine != null)
                Game.Instance.StopCoroutine(_rotatingCoroutine);
        }

        public void Return()
        {
            Stop();
            _rotatingCoroutine = Game.Instance.StartCoroutine(Rotating(_lastRotation, 0.12f));
        }

        private IEnumerator Rotating(Quaternion rotation, float time)
        {
            float elapsedTime = 0;
            float lerp = 0f;
            var startRotation = _transform.rotation;
            IsRotating = true;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                lerp = elapsedTime / time;
                _transform.rotation = Quaternion.Slerp(startRotation, rotation, lerp);
                yield return null;
            }

            IsRotating = false;
            _transform.rotation = rotation;
            _lastRotation = rotation;
        }
    }

    [Serializable]
    public class ShapeMovement
    {
        [SerializeField] private AnimationCurve _moveEase;

        private Transform _transform;
        private Coroutine _movingCoroutine;
        public Vector3 LastPos { get; private set; }

        public bool IsMoving { get; private set; }  

        public void Construct(Transform transform)
        {
            _transform = transform;
            LastPos = transform.position;
        }

        public Vector3 Move(Vector3 direction)
        {
            direction.x *= 0.5f;
            direction.z *= 0.5f;
            var position = _transform.position + direction;
            Stop();
            _movingCoroutine = Game.Instance.StartCoroutine(Moving(position, 0.1f));

            return position;
        }
        
        public void MoveTo(Vector3 position)
        {
            Stop();
            _movingCoroutine = Game.Instance.StartCoroutine(Moving(position, 0.1f));
        }

        public void Stop()
        {
            if(_movingCoroutine != null)
                Game.Instance.StopCoroutine(_movingCoroutine);
        }

        public void Return()
        {
            Stop();
            _movingCoroutine = Game.Instance.StartCoroutine(Moving(LastPos, 0.06f));
        }

        private IEnumerator Moving(Vector3 position, float time)
        {
            float elapsedTime = 0;
            float lerp = 0f;
            var startPosition = _transform.position;
            IsMoving = true;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                lerp = elapsedTime / time;
                _transform.position = Vector3.Lerp(startPosition, position, lerp);
                yield return null;
            }

            IsMoving = false;
            _transform.position = position;
            LastPos = position;
        }
    }
}
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    [Serializable]
    public class GroundProjection
    {
        [SerializeField] private LayerMask _layerMask;

        private ITrigger _trigger;
        private float _halfHeight;
        private Transform _projectionPoint;

        public bool IsEnoughSpace => _trigger.IsTriggering == false;

        public void Construct(ITrigger trigger, Transform projectionPoint)
        {
            _projectionPoint = projectionPoint;
            _trigger = trigger;

            if (Physics.Raycast(_trigger.transform.position, Vector3.down, out RaycastHit hitInfo, 10f, _layerMask))
                _halfHeight = hitInfo.distance;
        }

        public void Update()
        {
            if (Physics.Raycast(_projectionPoint.position, Vector3.down, out RaycastHit hitInfo, 10f, _layerMask))
                _trigger.transform.position = hitInfo.point + Vector3.up* _halfHeight;
        }
    }


    interface IGrab
    {
        public Transform transform { get; }
        public bool CanPlace();
        public void Grab();
        public void Place();
        public void Return();
        public void SetPosition(Vector3 position);
    }

    public class Shape : MonoBehaviour, IGrab
    {

        [SerializeField] private ShapeMovement _movement;
        [SerializeField] private ShapeRotation _rotation;
        [SerializeField] private GroundProjection _groundProjection;

        private bool _choosen;
        private bool _placed;
        private Vector3 _defaultPosition;
        private Vector3 _offset;
        private MeshRenderer _renderer;

        public void Construct()
        {
            var vectorInt = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            _offset = transform.position - vectorInt;

            _movement.Construct(transform);
            _rotation.Construct(transform);
            _defaultPosition = transform.position;
            _renderer = GetComponent<MeshRenderer>();
            _groundProjection.Construct(GetComponentInChildren<ITrigger>(),transform);
        }

        [ContextMenu(nameof(DarkMagic))]
        private void DarkMagic()
        {
            var rb = gameObject.AddComponent<Rigidbody>();

            rb.isKinematic = true;
            rb.useGravity = false;

            var collisionCollider = new GameObject("CollisionCollider");
            collisionCollider.transform.SetParent(transform, false);
            collisionCollider.transform.localScale = Vector3.one*0.95f;
            var renderer = collisionCollider.AddComponent<MeshRenderer>();
            var filter = collisionCollider.AddComponent<MeshFilter>();
            var currentRender = GetComponent<MeshRenderer>();
            var currentFilter = GetComponent<MeshFilter>();
            renderer.sharedMaterial = currentRender.sharedMaterial;
            filter.sharedMesh = currentFilter.sharedMesh;

            collisionCollider.AddComponent<MeshCollider>()
                             .SetTrigger(true);

            DestroyImmediate(renderer);
            DestroyImmediate(filter);

            var groundProjection = Instantiate(collisionCollider, transform);
            groundProjection.name = "GroundProjection";
            groundProjection.transform.localScale = Vector3.one *0.98f;
            groundProjection.AddComponent<Trigger>();

            EditorUtility.SetDirty(gameObject);
        }

        public void Return()
        {
            _choosen = false;

            if (_placed == false)
                _movement.MoveTo(_defaultPosition);
        }

        public bool CanPlace()
        {
            return _groundProjection.IsEnoughSpace;
        }

        public void Place()
        {
            _movement.Lift(0);
            _placed = true;
            _choosen = false;
        }

        public void Grab()
        {
            _choosen = true;
            _placed = false;
            _movement.Lift(2);
        }

        public void SetPosition(Vector3 position)
        {
            var step = 0.2f;
            var x = position.x % step;
            var y = transform.position.y;
            var z = position.z % step;
            var convertedPosition  = position - new Vector3(x, y, z);
            convertedPosition.y = transform.position.y;
            transform.position = convertedPosition;
        }

        public void Update()
        {

            _groundProjection.Update();

            if( _choosen == false )
                return;


            if (_movement.IsMoving || _rotation.IsRotating)
                return;



            if (Input.GetKeyDown(KeyCode.R))
                _rotation.Rotate(Vector3.forward*4);
            if(Input.GetKeyDown(KeyCode.Q))
                _rotation.Rotate(Vector3.down);
            if(Input.GetKeyDown(KeyCode.E))
                _rotation.Rotate(Vector3.up);
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

        public void Lift(float height)
        {
            Stop();
            _movingCoroutine = Game.Instance.StartCoroutine(Lifting(height, 0.2f));
        }

        public void MoveTo(Vector3 position)
        {
            Stop();
            _movingCoroutine = Game.Instance.StartCoroutine(Moving(position, 0.1f));
        }

        public void Stop()
        {
            if (_movingCoroutine != null)
                Game.Instance.StopCoroutine(_movingCoroutine);
        }

        public void Return()
        {
            Stop();
            _movingCoroutine = Game.Instance.StartCoroutine(Moving(LastPos, 0.06f));
        }

        private IEnumerator Lifting(float height, float time)
        {
            float elapsedTime = 0;
            float lerp = 0f;
            var startHeight = _transform.position.y;
            var position = _transform.position;
            IsMoving = true;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                lerp = elapsedTime / time;
                position = _transform.position;
                position.y = Mathf.Lerp(startHeight,height,lerp);

                _transform.position = position;
                yield return null;
            }

            IsMoving = false;
            _transform.position = position;
            LastPos = position;
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
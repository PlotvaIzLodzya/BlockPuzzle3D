using Assets.BlockPuzzle.Controll;
using Assets.BlockPuzzle.Dependency;
using Assets.BlockPuzzle.View;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle.Puzzles
{

    public interface IShape
    {
        public bool Placed { get; }

        public void Construct(PuzzleDependency dependency);

        public void OnComplete();
    }

    public class Shape : MonoBehaviour, IGrab, IHighlightable, IShape
    {
        [SerializeField] private ShapeMovement _movement;
        [SerializeField] private ShapeRotation _rotation;
        [SerializeField] private GroundProjection _groundProjection;
        
        private LayerMask _LevelComplitionMask;
        private bool _choosen;
        private Vector3 _defaultPosition;
        private Quaternion _defaultRotation;
        private Vector3 _offset;
        private MeshRenderer _renderer;
        private float _stepAngle;
        private float _stepSize;

        public bool IsRequireHighlight { get;private set; }
        public bool Placed { get; private set; }
        public bool CanBeGrabbed { get; private set; }

        public void Construct(PuzzleDependency puzzleDependency)
        {
            CanBeGrabbed = true;
            var vectorInt = new Vector3((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
            _stepSize = puzzleDependency.StepSize;
            _offset = transform.position - vectorInt;
            _LevelComplitionMask = puzzleDependency.Masks.LevelComplition;
            _stepAngle = puzzleDependency.StepAngle;
            _movement.Construct(transform);
            _rotation.Construct(transform, puzzleDependency.RotationTime);
            _defaultPosition = transform.position;
            _defaultRotation = transform.rotation;
            _renderer = GetComponent<MeshRenderer>();
            _groundProjection = new GroundProjection(GetComponentInChildren<ITrigger>(), transform, puzzleDependency.Masks.Ground);
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
            groundProjection.AddComponent<Projection>();

            gameObject.AddComponent<GlowObject>();

            EditorUtility.SetDirty(gameObject);
        }

        public void OnComplete()
        {
            IsRequireHighlight = false;
            CanBeGrabbed = false;
        }

        public void Return()
        {
            _choosen = false;

            IsRequireHighlight = false;

            if (Placed == false)
            {
                _movement.MoveTo(_defaultPosition);
                _rotation.RotateTo(_defaultRotation);
            }
        }

        public bool CanPlace()
        {
            var isProperPlace = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f, _LevelComplitionMask);

            return _groundProjection.IsEnoughSpace && isProperPlace;
        }

        public void Place()
        {
            _movement.Lift(0);
            IsRequireHighlight = false;
            Placed = true;
            _choosen = false;
        }

        public void Grab()
        {
            IsRequireHighlight = true;
            _choosen = true;
            Placed = false;
            _movement.Lift(2);
        }

        public void SetPosition(Vector3 position)
        {
            var step = _stepSize;
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
                _rotation.Rotate(Vector3.forward,180);
            if(Input.GetKeyDown(KeyCode.Q))
                _rotation.Rotate(Vector3.down, _stepAngle);
            if(Input.GetKeyDown(KeyCode.E))
                _rotation.Rotate(Vector3.up, _stepAngle);
        }
    }

    [Serializable]
    public class ShapeRotation
    {
        [SerializeField] private AnimationCurve _rotatuibEase;

        private Transform _transform;
        private Coroutine _rotatingCoroutine;
        private Quaternion _lastRotation;
        private float _rotationTime;

        public bool IsRotating { get; private set; }

        public void Construct(Transform transform, float rotationTime)
        {
            _transform = transform;
            _rotationTime = rotationTime;
            _lastRotation = transform.rotation;
        }

        public void Rotate(Vector3 direction, float angleStep)
        {
            var angle = direction * angleStep;
            var position = _transform.rotation * Quaternion.Euler(angle);
            Stop();
            _rotatingCoroutine = Game.CoroutineHandler.StartCoroutine(Rotating(position, _rotationTime));
        }

        public void RotateTo(Quaternion rotation)
        {
            Stop();
            _rotatingCoroutine = Game.CoroutineHandler.StartCoroutine(Rotating(rotation, 0.06f));
        }

        public void Stop()
        {
            if (_rotatingCoroutine != null)
                Game.CoroutineHandler.StopCoroutine(_rotatingCoroutine);
        }

        public void Return()
        {
            Stop();
            _rotatingCoroutine = Game.CoroutineHandler.StartCoroutine(Rotating(_lastRotation, 0.12f));
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
            _movingCoroutine = Game.CoroutineHandler.StartCoroutine(Lifting(height, 0.2f));
        }

        public void MoveTo(Vector3 position)
        {
            Stop();
            _movingCoroutine = Game.CoroutineHandler.StartCoroutine(Moving(position, 0.1f));
        }

        public void Stop()
        {
            if (_movingCoroutine != null)
                Game.CoroutineHandler.StopCoroutine(_movingCoroutine);
        }

        public void Return()
        {
            Stop();
            _movingCoroutine = Game.CoroutineHandler.StartCoroutine(Moving(LastPos, 0.06f));
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
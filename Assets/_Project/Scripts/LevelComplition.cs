using Assets.BlockPuzzle.Puzzles;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.BlockPuzzle.Complition
{
    public class LevelComplition : MonoBehaviour, IComplition
    {
        [SerializeField] private Trigger _complitionTrigger;

        private List<Shape> _shapes = new List<Shape>();
        private int _totalShapeCount;

        public bool Completed => _shapes.Count >= _totalShapeCount;
        public event Action OnChange;

        public void Construct(int totalShapeCount)
        {
            _totalShapeCount = totalShapeCount;

            if(GetComponent<Rigidbody>() == null)
            {
                transform.AddComponent<Rigidbody>().isKinematic = true;
            }
        }

        private void OnEnable()
        {
            _complitionTrigger.ColliderEntered += OnColliderEntered;
            _complitionTrigger.ColliderExit += OnColliderExit;
        }

        private void OnDisable()
        {
            _complitionTrigger.ColliderEntered -= OnColliderEntered;
            _complitionTrigger.ColliderExit -= OnColliderExit;
        }

        private void OnColliderEntered(Collider other)
        {
            if(other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out Shape shape) && shape.Placed)
            {
                if(_shapes.Contains(shape) == false)
                {
                    _shapes.Add(shape);
                }
            }

            OnPlace();
        }

        private void OnColliderExit(Collider other)
        {
            if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out Shape shape))
            {
                if (_shapes.Contains(shape))
                {
                    _shapes.Remove(shape);
                }
            }
        }

        private void OnPlace()
        {
            OnChange?.Invoke();
        }
    }
}


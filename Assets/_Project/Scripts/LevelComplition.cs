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

        private List<IShape> _shapes = new List<IShape>();
        private int _totalShapeCount;
        public string _guid;

        public bool IsCompleted => _shapes.Count >= _totalShapeCount;
        public event Action OnChange;

        public void Construct(int totalShapeCount, string guid)
        {
            _totalShapeCount = totalShapeCount;
            _guid = guid;

            if (GetComponent<Rigidbody>() == null)
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
            if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out IShape shape) && shape.Placed)
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
            if (other.attachedRigidbody != null && other.attachedRigidbody.TryGetComponent(out IShape shape))
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

            if (IsCompleted)
                SaveService.Save(_guid, 0);
        }
    }
}


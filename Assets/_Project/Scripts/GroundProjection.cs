using System;
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
}
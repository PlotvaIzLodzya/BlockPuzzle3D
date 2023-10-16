using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{

    public class GroundProjection
    {
        private LayerMask _groundMask;
        private ITrigger _trigger;
        private float _halfHeight;
        private Transform _projectionPoint;

        public bool IsEnoughSpace => _trigger.IsTriggering == false;

        public GroundProjection(ITrigger trigger, Transform projectionPoint, LayerMask groundMask)
        {
            _groundMask = groundMask;
            _projectionPoint = projectionPoint;
            _trigger = trigger;

            if (Physics.Raycast(_trigger.transform.position, Vector3.down, out RaycastHit hitInfo, 10f, _groundMask))
                _halfHeight = hitInfo.distance;
        }

        public void Update()
        {
            if (Physics.Raycast(_projectionPoint.position, Vector3.down, out RaycastHit hitInfo, 10f, _groundMask))
                _trigger.transform.position = hitInfo.point + Vector3.up* _halfHeight;
        }
    }
}
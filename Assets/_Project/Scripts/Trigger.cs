using System;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Trigger : MonoBehaviour, ITrigger
    {
        public bool IsTriggering { get; private set; }
        public bool IsTriggered { get; private set; }

        public event Action<Collider> ColliderEntered;
        public event Action<Collider> ColliderExit;

        private void OnTriggerStay(Collider other)
        {
            IsTriggering = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            ColliderEntered?.Invoke(other);
            IsTriggered = true;   
        }

        private void OnTriggerExit(Collider other)
        {
            IsTriggered = false;
            IsTriggering = false;
            ColliderExit?.Invoke(other);
        }

        private void Update()
        {
            IsTriggered = false;
        }
    }
}
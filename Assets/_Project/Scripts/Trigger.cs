using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class Trigger : MonoBehaviour
    {
        public bool IsTriggering { get; private set; }
        public bool IsTriggered { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            IsTriggering = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            IsTriggered = true;   
        }

        private void OnTriggerExit(Collider other)
        {
            IsTriggered = false;
            IsTriggering = false; 
        }
    }
}
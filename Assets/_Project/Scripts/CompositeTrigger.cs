using System.Linq;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public class CompositeTrigger : MonoBehaviour, ITrigger
    {
        [SerializeField] private Trigger[] _triggers;

        public bool IsTriggering => _triggers.Any(trigger => trigger.IsTriggering);

        public bool IsTriggered => _triggers.Any(trigger => trigger.IsTriggered);

        [ContextMenu(nameof(GetFromChildren))]
        public void GetFromChildren()
        {
            _triggers = GetComponentsInChildren<Trigger>();
        }
    }
}
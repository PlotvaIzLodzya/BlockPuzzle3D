using UnityEngine;

namespace Assets.BlockPuzzle
{
    public static class Extensions
    {
        public static MeshCollider SetTrigger( this MeshCollider collider, bool isTrigger)
        {
            collider.convex = isTrigger;
            collider.isTrigger = isTrigger;

            return collider;
        }
    }
}
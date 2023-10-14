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

        public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
        public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
    }
}
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.BlockPuzzle.Extensions
{
    [Serializable]
    public struct RectangleArea
    {
        public Vector2 Size { get; private set; }
        public Vector2 LeftCorner;
        public Vector2 RightCorner;

        public RectangleArea(Vector2 size)
        {
            Size = size;
            LeftCorner = Vector2.zero;
            RightCorner = Size;
        }

        public RectangleArea(float x, float y)
        {
            Size = new Vector2(x, y);
            LeftCorner = Vector2.zero;
            RightCorner = Size;

        }

        public RectangleArea(Vector2 leftCorner, Vector2 rightCorner)
        {
            LeftCorner = leftCorner;
            RightCorner = rightCorner;
            //Мог наебаться
            Size = new Vector2(rightCorner.x - leftCorner.x, rightCorner.y - leftCorner.y);
        }

        public Vector2 GetRandomPosition()
        {
            var x = UnityEngine.Random.Range(LeftCorner.x, RightCorner.x);
            var y = UnityEngine.Random.Range(LeftCorner.y, RightCorner.y);

            return new Vector2(x, y);
        }

        public bool Contains(Vector2 position)
        {
            return position.x > LeftCorner.x
                && position.x < RightCorner.x
                && position.y > LeftCorner.y
                && position.y < RightCorner.y;
        }
    }

    public struct VectorEnhance
    {

        public static Vector2 ClampPosition(Vector2 vector, RectangleArea rectangleArea)
        {
            vector.x = Mathf.Clamp(vector.x, rectangleArea.LeftCorner.x, rectangleArea.RightCorner.x);
            vector.y = Mathf.Clamp(vector.y, rectangleArea.LeftCorner.y, rectangleArea.RightCorner.y);

            return vector;
        }

        public static Vector2 ClampPosition(Vector2 vector, Vector2 min, Vector2 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);

            return vector;
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float min, float max)
        {
            vector = Vector2.ClampMagnitude(vector, max);
            vector = ClampMagnitude(vector, min);

            return vector;
        }

        public static Vector2 ClampMagnitude(Vector2 vector, float minLength)
        {
            float num = vector.sqrMagnitude;
            if (num < minLength * minLength)
            {
                float num2 = (float)Math.Sqrt(num);
                float num3 = vector.x / num2;
                float num4 = vector.y / num2;
                return new Vector2(num3 * minLength, num4 * minLength);
            }

            return vector;
        }
    }

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

#if(UNITY_EDITOR)

    public class ReadOnlyAttribute : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif
}
using TMPro;
using UnityEngine;

namespace Assets.BlockPuzzle
{
    public static class StringExtensions
    {
        public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
        public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";       
    }

    public class Game: MonoBehaviour
    {
        public static Game Instance { get; private set; }
        //public TMP_Text _text;

        private void Awake()
        {
            //_text.text = $"" +
            //$"{"H".AddColor(Color.red)}" +
            //$"{"E".AddColor(Color.blue)}" +
            //$"{"L".AddColor(Color.green)}" +
            //$"{"L".AddColor(Color.white)}" +
            //$"{"O".AddColor(Color.yellow)}";

            //_text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
            Instance = this;
        }
    }
}


using UnityEngine;

namespace Assets.BlockPuzzle.SaveLoad
{
    public static class SaveService
    {
        public static void Save(string guid, int value)
        {
            PlayerPrefs.SetInt(guid, value);
        }

        public static bool HasSave(string guid)
        {
            return PlayerPrefs.HasKey(guid);
        }
    }
}


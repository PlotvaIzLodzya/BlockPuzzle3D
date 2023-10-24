using Assets.BlockPuzzle.HUD;
using System;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

namespace Assets.BlockPuzzle.Localization
{
    [Serializable]
    public struct InGameText
    {
        public string EN;
        public string RU;
        public string TR;

        public string TranslatedLine => GetCurrentTranslate();

        public InGameText(string en, string ru, string tr)
        {
            EN = en;
            RU = ru;
            TR = tr;
        }

        public string GetCurrentTranslate()
        {
            var lang = "en";

            var line = lang switch
            {
                "en" => EN,
                "ru" => RU,
                "tr" => TR,

                _ => EN
            };

            return line;
        }
    }
}


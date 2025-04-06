using System;
using Agava.YandexGames;

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
            var lang = "ru";

#if (!UNITY_EDITOR)
    lang = YandexGamesSdk.Environment.i18n.lang;
#endif

            return lang switch
            {
                "en" => EN,
                "ru" => RU,
                "tr" => TR,

                _ => EN
            };
        }
    }
}


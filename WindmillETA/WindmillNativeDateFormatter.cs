using System;
using System.Collections.Generic;
using BokuMono;
using TMPro;
using UnityEngine;

namespace WindmillETA
{
    internal static class WindmillNativeDateFormatter
    {
        private static readonly Dictionary<(Language, int, int, int), string> Cache = new();
        private static readonly HashSet<Language> WarnedLanguages = new();

        public static bool TryFormat(BokuMonoDateTime date, LocalizedTextMeshPro context, out string result)
        {
            result = null;
            var languageManager = LanguageManager.Instance;
            if (context == null || languageManager == null || languageManager.CurrentLanguage == Language.None)
                return false;

            var language = languageManager.CurrentLanguage;
            var key = (language, date.Year, date.Month, date.Day);
            if (Cache.TryGetValue(key, out result)) return result != null;

            try
            {
                // The game's HUD date template supplies each language's word order.
                var template = languageManager.GetLocalizeText(
                    LocalizeTextTableType.TimeAndDateText, (uint)TimeAndDateTextId.TimeAndDate_107000, true);
                if (string.IsNullOrWhiteSpace(template))
                    throw new FormatException("The game's date template is empty.");

                var source = TextTagUtility.MakeDateConstTagString(date) + template;
                // GetText mutates the TMP supplied to it. Parsing against a live
                // timer and restoring only .text can still leave other UI state changed.
                // Use a disposable, inactive TMP; never pass the visible timer to it.
                GameObject scratchObject = null;
                try
                {
                    scratchObject = new GameObject("WindmillETA_DateParser");
                    scratchObject.SetActive(false);
                    var scratch = scratchObject.AddComponent<TextMeshProUGUI>();
                    scratch.font = context.font;
                    scratch.fontSharedMaterial = context.fontSharedMaterial;
                    scratch.fontSize = context.fontSize;
                    scratch.raycastTarget = false;
                    var textData = new TextData();
                    result = textData.GetText(scratch, source, null, Array.Empty<string>());
                }
                finally
                {
                    if (scratchObject != null) UnityEngine.Object.Destroy(scratchObject);
                }
                if (string.IsNullOrWhiteSpace(result) || result.Contains('<') || result.Contains('>'))
                    throw new FormatException($"Date expansion returned empty text or unresolved markup: {result}");

            }
            catch (Exception ex)
            {
                result = null;
                if (WarnedLanguages.Add(language))
                    Plugin.LogSource.LogWarning($"[ETA-DATE] Native date unavailable for {language}; using numeric game date (year/season/day). {ex.GetType().Name}: {ex.Message}");
            }

            // Bound both successful and failed attempts; no repeated parsing every frame.
            if (Cache.Count >= 128) Cache.Clear();
            Cache[key] = result;
            return result != null;
        }
    }
}

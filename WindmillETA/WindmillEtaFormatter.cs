using BokuMono;

namespace WindmillETA
{
    public static class WindmillEtaFormatter
    {
        public static string Format(BokuMonoDateTime now, BokuMonoDateTime end, LocalizedTextMeshPro context = null)
        {
            bool isToday =
                now.Year == end.Year &&
                now.Month == end.Month &&
                now.Day == end.Day;

            var time = FormatTime(end);
            if (isToday)
            {
                return time;
            }

            if (WindmillNativeDateFormatter.TryFormat(end, context, out var date))
            {
                return date + " " + time;
            }

            // Last-resort numeric game date; no language files are needed.
            return string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "{0}/{1}/{2} {3}", end.Year, end.Month, end.Day, time);
        }

        private static string FormatTime(BokuMonoDateTime end)
        {
            // Read the live game setting on each update. Do not depend on TimeFormat
            // or cache a clock string that could outlive a notation/language change.
            var notation = OptionManager.Instance?.OptionSystem?.OptionTimeNotation;
            var use12Hour = notation != null &&
                notation.TimeNotationType == OptionSetting.TimeNotationType._12Hour;
            string period = null;
            if (use12Hour)
            {
                var languageManager = LanguageManager.Instance;
                if (languageManager != null)
                    period = languageManager.GetDateTimeText(end.Hour < 12
                        ? LanguageManager.DateTime.Am : LanguageManager.DateTime.Pm);
            }

            // Until the setting is available (or if it is None), retain the old 24h display.
            return WindmillClockText.Format(end.Hour, end.Minute, use12Hour, period);
        }
    }
}

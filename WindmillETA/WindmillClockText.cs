using System.Globalization;

namespace WindmillETA
{
    internal static class WindmillClockText
    {
        public static string Format(int hour, int minute, bool use12Hour, string period)
        {
            if (!use12Hour)
                return string.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}", hour, minute);

            var displayHour = hour % 12;
            if (displayHour == 0) displayHour = 12;
            if (string.IsNullOrWhiteSpace(period)) period = hour < 12 ? "AM" : "PM";
            return string.Format(CultureInfo.InvariantCulture, "{0}:{1:D2} {2}", displayHour, minute, period.Trim());
        }
    }
}

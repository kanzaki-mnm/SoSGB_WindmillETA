using System;

namespace WindmillETA
{
    // Only accept calculations made during this dialog update, never stale results
    // from a recipe list or a previous frame. Nested updates restore their parent.
    internal sealed class WindmillDurationCapture : IDisposable
    {
        [ThreadStatic] private static WindmillDurationCapture current;
        private readonly WindmillDurationCapture parent;
        private readonly int expectedTime;
        public int? Minutes { get; private set; }

        public WindmillDurationCapture(int expectedTime)
        {
            this.expectedTime = expectedTime;
            parent = current;
            current = this;
        }

        public static void Record(int time, int day, int hour, int minute)
        {
            if (current == null || time != current.expectedTime) return;
            long total = (long)day * 1440 + (long)hour * 60 + minute;
            current.Minutes = day >= 0 && hour >= 0 && minute >= 0 && total <= int.MaxValue
                ? (int)total : null;
        }

        public void Dispose()
        {
            if (current == this) current = parent;
        }
    }
}

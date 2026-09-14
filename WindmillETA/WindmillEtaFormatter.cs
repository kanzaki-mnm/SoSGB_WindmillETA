using BokuMono;

namespace WindmillETA
{
    public static class WindmillEtaFormatter
    {
        public static string Format(BokuMonoDateTime now, BokuMonoDateTime end)
        {
            bool isToday =
                now.Year == end.Year &&
                now.Month == end.Month &&
                now.Day == end.Day;

            if (isToday)
            {
                // 今日中に完成する場合は日付を省略
                return $"{end.Hour:D2}:{end.Minute:D2}";
            }

            var season = GetSeasonName(end.Month);
            var dayOfWeek = GetDayOfWeekName(end.DayOfWeek);

            // 完成が明日以降の場合は季節と曜日を表示
            return $"{season}{end.Day}日({dayOfWeek}) {end.Hour:D2}:{end.Minute:D2}";
        }

        private static string GetSeasonName(int month)
        {
            return month switch
            {
                1 => "はる",
                2 => "なつ",
                3 => "あき",
                4 => "ふゆ",
                _ => "?"
            };
        }

        private static string GetDayOfWeekName(Il2CppSystem.DayOfWeek day)
        {
            return day switch
            {
                Il2CppSystem.DayOfWeek.Sunday => "日",
                Il2CppSystem.DayOfWeek.Monday => "月",
                Il2CppSystem.DayOfWeek.Tuesday => "火",
                Il2CppSystem.DayOfWeek.Wednesday => "水",
                Il2CppSystem.DayOfWeek.Thursday => "木",
                Il2CppSystem.DayOfWeek.Friday => "金",
                Il2CppSystem.DayOfWeek.Saturday => "土",
                _ => "?"
            };
        }
    }
}
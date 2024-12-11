using System;

namespace Utils
{
    public static class Time
    {
        // 현재 시간 가져오기 (추후 서버 시간 가져오기)
        public static DateTime GetCurrent()
        {
            DateTime current = new DateTime();

            return current;
        }

        // 남은 시간 계산 (현재시간 기준)
        public static TimeSpan GetGapTime(DateTime endTime)
        {
            var gapTime = endTime - GetCurrent();

            if (gapTime.CompareTo(TimeSpan.Zero) > 0)
            {
                return gapTime;
            }
            else
            {
                return TimeSpan.Zero;
            }
        }

        public static void AddTime(ref DateTime time, int hour, int minute, int second)
        {
            time.AddHours(hour);
            time.AddMinutes(minute);
            time.AddSeconds(second);
        }
    }
}
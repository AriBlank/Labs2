using System.Collections.Generic;

namespace mylibr;

public static class DayGenerator
{
public static IEnumerable<string> GenerateDays()
        {
            string[] days =
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };

            int index = 0;

            while (true)
            {
                yield return days[index];
                index = (index + 1) % days.Length;
            }
        }
}

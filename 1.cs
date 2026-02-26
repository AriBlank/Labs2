using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


    static void Main()
    {
        var generator = CyclicDaysGenerator();
        ConsumeWithTimeout(generator, 5);
    }

    public static IEnumerable<string> CyclicDaysGenerator()
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

    public static void ConsumeWithTimeout(IEnumerable<string> generator, int seconds)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        foreach (var day in generator)
        {
            if (stopwatch.Elapsed.TotalSeconds >= seconds)
                break;

            Console.WriteLine($"{DateTime.Now:T} - {day}");
            Thread.Sleep(500);
        }
    }

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        var generator = DayGenerator();
        TimeoutIterator(generator, 5);
    }

    static IEnumerable<string> DayGenerator()
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

    static void TimeoutIterator(IEnumerable<string> generator, int seconds)
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
}
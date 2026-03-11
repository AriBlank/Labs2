using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace mylibr
{
    public static class TimeoutIter
    {
        public static void Run(IEnumerable<string> generator, int seconds)
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
}
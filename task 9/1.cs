using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

public enum LogLevel
{
    Info,
    Debug,
    Error
}
public static class Log
{
    public static Action<string> WriteLine { get; set; } = Console.WriteLine;
    public static Func<T> Decorate<T>(Func<T> func, LogLevel level)
    {
        return () =>
        {
            var sw = Stopwatch.StartNew();
            try
            {
                if (level != LogLevel.Error)
                    WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [{level}] enter: {func.Method.Name}");

                var result = func();

                if (level != LogLevel.Error)
                    WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [{level}] exit: {result} [{sw.ElapsedMilliseconds}ms]");

                return result;
            }
            catch (Exception ex)
            {
                WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] [ERROR] {func.Method.Name}: {ex.Message}");
                throw;
            }
        };
    }
}

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
}

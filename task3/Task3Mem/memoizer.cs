using System;
using System.Collections.Generic;

namespace Task3Mem;

public class Memoizer<TInput, TOutput> where TInput : notnull
{
    private Dictionary<TInput, TOutput> cache = new();

    private Func<TInput,TOutput> func;

    public Memoizer(Func<TInput, TOutput> func)
    {
        this.func = func;
    }

    public TOutput Invoke(TInput input)
    {
        if (cache.ContainsKey(input))
        {
            Console.WriteLine("From cache");
            return cache[input];
        }

        Console.WriteLine("Calculating in process");
        var result = func(input);
        cache[input] =result;
        return result;
    }
}
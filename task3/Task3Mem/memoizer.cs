using System;
using System.Collections.Generic;

namespace Task3Mem;

public class Memoizer<TInput, TOutput> where TInput : notnull
{
    private Dictionary<TInput, TOutput> cache = new();
    private Func<TInput,TOutput> func;
    private int MaxCacheSize;

    public Memoizer(Func<TInput, TOutput> func, int MaxCacheSize)
    {
        this.func = func;
        this.MaxCacheSize = MaxCacheSize;
    }

    public TOutput Invoke(TInput input)
    {
       if (cache.TryGetValue(input, out var result))
        {
            Console.WriteLine("From cache");
            return result;
        }

        Console.WriteLine("Calculating in process");
        result = func(input);

        if(cache.Count >= MaxCacheSize)
        {
            var firstKey =default(TInput)!;
            foreach (var key in cache.Keys)
            {
                firstKey= key;
                break;
            }

            cache.Remove(firstKey);
        }
        
        cache[input] = result;
        return result;
    }
}
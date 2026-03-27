using System;

namespace Task3Mem;

class Program
{
    static void Main()
    {
        Func<int, int> cube = x => x*x*x;

        var memo = new Memoizer<int, int>(cube, 5);

        while(true) {
            Console.Write("Enter the argument for function:");
            int x = int.Parse(Console.ReadLine()!); //перетворює введені дані типу string в int

            var result = memo.Invoke(x);
            Console.WriteLine(result);
        }
    }
}
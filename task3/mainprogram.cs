class Program
{
    static void Main()
    {
        Func<int, int> cube = x => x*x*x;

        var memo = new Memoizer<int, int>(cube);

    }
}
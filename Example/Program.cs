using mylibr;

class Program
{
    static void Main()
    {
        var generator = DayGenerator.GenerateDays();
        TimeoutIter.Run(generator, 5);
    }
}

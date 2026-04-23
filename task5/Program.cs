using System;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
public class asyncron
{
    public int[] numbers = [1, 4, 20, 5, 7];

    public void doAction(int[] nums, Action<int[]> callback) => callback(nums);
    public void Square(int[] arr) {
        int[] squared = arr.Select(x => x*x).ToArray();
        Console.WriteLine("squared array: ");
        Console.Write(string.Join(", ", squared));
    }

    public int[] SquareArr(int[] arr) {
        Console.WriteLine("squared array: ");
        int[] squared = arr.Select(x => x*x).ToArray();
        return squared;
    }


    public async Task asyncAction(int[] arr, CancellationToken token)
    {
        Console.WriteLine("squared array: ");
        await Task.Delay(2000, token);

        int[] result = SquareArr(arr);
        Console.Write(string.Join(", ", result));
    }

    public class Program
    {
        static async Task Main()
        {
            var cts = new CancellationTokenSource(); 
            CancellationToken token = cts.Token;


            asyncron nAsync = new asyncron();
            Action<int[]> nSquare = nAsync.Square;
            nAsync.doAction(nAsync.numbers, nSquare); 

            Task task = nAsync.asyncAction(nAsync.numbers, token);
            await Task.Delay(1000);
            cts.Cancel();

            await task;
        }
    }
}

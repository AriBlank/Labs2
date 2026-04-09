using System;

public class Item
{
    public string Name { get; set; } 
    public int Priority { get; set; }
    public Item(string name, int priority) 
    {
        Name = name;
        Priority = priority;
    }
}
public class PriorityQueue
{
    private List<Item> items = new List<Item>();
    public void enqueue(string name, int priority) //додає елемент у чергу
    {
        items.Add(new Item(name, priority));
    }

    public void DeleteHighest() //видаляє елемент з найбільшим пріоритетом
    {
        Item maxIndex = items[0];

        foreach (var item in items)
        {
            if(item.Priority > maxIndex.Priority)
            {
                maxIndex = item;
            }
        }
        items.Remove(maxIndex);
    }

    public void DeleteLowest() //видаляє елемент з найменше пріоритетом
    {
        Item minIndex = items[0];

        foreach (var item in items)
        {
            if(item.Priority < minIndex.Priority)
            {
                minIndex = item;
            }
        }
        items.Remove(minIndex);
    }

    public void DeleteOldest()
    {
        items.Remove(items[0]);
    }

    public void DeleteNewest()
    {
        items.RemoveAt(items.Count - 1);
    }

    public void ShowQueue()
    {
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Name} ({item.Priority})");
        }
    }

    // public bool CheckForExist()
    // {
    //     if(items.Count == 0)
    //     {
    //         Console.WriteLine("Queue is empty");
    //         return false;
    //     }
    //     return true;
    // }
}

class Program
{
    static void Main()
    {
        var queue = new PriorityQueue();
        while(true){
            Console.WriteLine("Enter:\n 1 - add element\n 2 - delete highest\n 3 - delete lowest\n 4 - delete oldest\n 5 - delete newest\n 6 - show queue");

            int choice = int.Parse(Console.ReadLine());

            switch(choice)
            {
                case 1:
                    Console.WriteLine("Enter the name of element:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter the priority of element:");
                    int priority = int.Parse(Console.ReadLine());

                    queue.enqueue(name, priority) ;
                    break;

                case 2:
                    queue.DeleteHighest();
                    break;

                case 3:
                    queue.DeleteLowest();
                    break;

                case 4:
                    queue.DeleteOldest();
                    break;
            
                case 5:
                    queue.DeleteNewest();
                    break;

                case 6:
                    queue.ShowQueue();
                    break;
            }
        }
    } 
}
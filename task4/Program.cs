using System;
using System.Linq;

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
    private List<Item> maxPriorityItem = new List<Item>();

    private List<Item> minPriorityItem = new List<Item>();

    public void enqueue(string name, int priority) //додає елемент у чергу
    {
        var newItem = new Item(name, priority);
        items.Add(new Item(name, priority));

        int index = 0;
        foreach(var currentItem in maxPriorityItem)
        {
            if(newItem.Priority > currentItem.Priority )
            {
                break;
            }
            index++;
        }

        maxPriorityItem.Insert(index, newItem);

        // int indexForMin = 0;
        // foreach(var currentItem in minPriorityItem)
        // {
        //     if(currentItem.Priority > newItem.Priority)
        //     {
        //         break;
        //     }
        //     indexForMin++;
        // }

        // minPriorityItem.Insert(indexForMin, newItem);
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


    public void ShowQueue(int typeLists)
    {
        switch (typeLists)
        {
            case 1:
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Name} ({item.Priority})");
                }
                break;

            case 2:
                foreach (var item in maxPriorityItem)
                {
                    Console.WriteLine($"{item.Name} ({item.Priority})");
                }
                break;
        }
    }
}

class Program
{
    static void Main()
    {
        var queue = new PriorityQueue();
        while(true){
            Start:
            Console.WriteLine("Enter:\n 1 - add element\n 2 - delete highest\n 3 - delete lowest\n 4 - delete oldest\n 5 - delete newest\n 6 - show queue\n 7 - show ordered queue");
            
            string entryValue = Console.ReadLine();
            bool allNumbers = entryValue.All(char.IsDigit);

            if(allNumbers == false || string.IsNullOrWhiteSpace(entryValue) == true)
            {
                Console.WriteLine("any options were not choosen or input was invalid");
                
                goto Start;
            }

            int choice = int.Parse(entryValue);

            
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
                    queue.ShowQueue(1);
                    break;

                case 7: 
                    queue.ShowQueue(2);
                    break;

                default:
                    Console.WriteLine("not valid number");
                    break;
            }
        }
    } 
}
using System;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Threading;

public class Message
{
    public string Sender { get; }
    public string Content { get; }
    public DateTime Timestamp { get; }
    public Message(string sender, string content)
    {
        Sender = sender;
        Content = content;
        Timestamp = DateTime.Now;
    }
    public override string ToString() =>
        $"[{Timestamp:HH:mm:ss}] from {Sender}: {Content}";
}
public class MessageBroker
{   
    private readonly Subject<Message> _subject = new Subject<Message>();
    public void SendMessage(Message message)
    {
        Console.WriteLine($"[BROKER] message was sent: {message}");
        _subject.OnNext(message);
    }
    public IDisposable Subscribe(Action<Message> onNext)
    {
        return _subject.Subscribe(onNext);
    }
    public IDisposable SubscribeFrom(string sender, Action<Message> onNext)
    {
        return _subject
            .Where(msg => msg.Sender == sender)
            .Subscribe(onNext);
    }
}
public class Entity
{
    public string Name { get; }
    private readonly MessageBroker _broker;
    private IDisposable _subscription;

    public Entity(string name, MessageBroker broker)
    {
        Name = name;
        _broker = broker;
    }
    public void SubscribeAll()
    {
        _subscription = _broker.Subscribe(OnMessageReceived);
        Console.WriteLine($"[{Name}] subscriibed on messages");
    }
    public void SubscribeFrom(string sender)
    {
        _subscription = _broker.SubscribeFrom(sender, OnMessageReceived);
        Console.WriteLine($"[{Name}] subscibed on messages by {sender}.");
    }
    public void Unsubscribe()
    {
        _subscription?.Dispose();
        _subscription = null;
        Console.WriteLine($"[{Name}] unsubscribed from messages");
    }
    private void OnMessageReceived(Message message)
    {
        Console.WriteLine($"[{Name}] got: {message}");
    }
    public void SendMessage(string content)
    {
        var msg = new Message(Name, content);
        Console.WriteLine($"[{Name}] send: {content}");
        _broker.SendMessage(msg);
    }
    
}
public static class Program
{
    public static void Main()
    {
        var broker = new MessageBroker();

        var hatsune = new Entity("Hatsune", broker);
        var zxcgul = new Entity("ZXCGul", broker);
        var viego = new Entity("viego", broker);
        var dave = new Entity("dave", broker);

        hatsune.SubscribeAll();              
        zxcgul.SubscribeFrom("Hatsune");      
        viego.SubscribeAll(); 

        Console.WriteLine("\n sending a message from Hatsune");
        hatsune.SendMessage("green onion");

        Thread.Sleep(500);

        Console.WriteLine("\n sending a message from ZXCGul");
        zxcgul.SendMessage("1000-7");

        Thread.Sleep(500);

        Console.WriteLine("\ndave subscribing on all messages");
        dave.SubscribeAll();

        Thread.Sleep(500);

        Console.WriteLine("\nsending a message from viego");
        viego.SendMessage("oh, Isolde");

        Thread.Sleep(500);

        Console.WriteLine("\nZXCGul unsubscribing");
        zxcgul.Unsubscribe();

        Thread.Sleep(500);

        Console.WriteLine("\nsending a message from Hatsune after ZXCGul unsubscribed");
        hatsune.SendMessage("zxcgul cannot see this message");

  
        Console.WriteLine("\npress any key to stop");
        Console.ReadKey();
    }
}
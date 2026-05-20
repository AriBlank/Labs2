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
        $"[{Timestamp:HH:mm:ss}] от {Sender}: {Content}";
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
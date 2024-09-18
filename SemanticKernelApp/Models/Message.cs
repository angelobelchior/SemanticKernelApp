namespace SemanticKernelApp.Models;

public class Message
{
    public Sender Sender { get; } = Sender.Agent;
    public string Text { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public Message()
    {
    }

    private Message(Sender sender, string text, string user)
    {
        Sender = sender;
        Text = text;
        User = user;
        DateTime = DateTime.Now;
    }

    public static Message FromUser(string text, string user)
        => new(Sender.User, text, user);
    
    public static Message FromAgent(string text)
        => new(Sender.Agent, text, "Agent");
}

public enum Sender
{
    User,
    Agent
}
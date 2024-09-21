namespace SemanticKernelApp.Models;

public record User(int Id, string Name, int Age, string Role)
{
    public override string ToString()
    {
        var message = $"Meu nome é {Name}, meu Id é {Id}, tenho {Age} anos e sou {Role}.";
        return message;
    }
}
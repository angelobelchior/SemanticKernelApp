using System.Text.Json;

namespace SemanticKernelDemo.Models;

public record User(int Id, string Name, int Age, string Role)
{
    public override string ToString()
    {
        var message = new { Message = $"Meu nome é {Name}, meu Id é {Id}, tenho {Age} anos e sou {Role}." };
        return JsonSerializer.Serialize(message);
    }
}
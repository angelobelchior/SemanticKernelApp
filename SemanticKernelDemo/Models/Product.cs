
namespace SemanticKernelDemo.Models;

public record Product(int Id, string Name, decimal Price, DateTime DateTime);

public record Target(decimal Value, decimal TotalSellToday);
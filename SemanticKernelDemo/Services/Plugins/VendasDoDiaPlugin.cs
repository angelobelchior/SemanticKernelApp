using System.ComponentModel;
using Microsoft.SemanticKernel;
using SemanticKernelDemo.Models;

namespace SemanticKernelDemo.Services.Plugins;

public class VendasDoDiaPlugin
{
    [KernelFunction]
    [Description("Obtém as vendas do dia feitas pelo usuário")]
    [return: Description("Lista de Produtos Vendidos no dia de hoje.")]
    public IEnumerable<Product> ObterAsVendasDoDia()
    {
        var produtos = new List<Product>();
        produtos.Add(new (1, "PS5", 5000, DateTime.Now));
        produtos.Add(new (2, "XBOX", 4000, DateTime.Now));
        produtos.Add(new (3, "God of War", 350, DateTime.Now));
        produtos.Add(new (4, "The Last of Us", 250, DateTime.Now));
        return produtos;
    }
}
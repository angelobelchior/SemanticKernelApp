using System.ComponentModel;
using Microsoft.SemanticKernel;
using SemanticKernelDemo.Models;

namespace SemanticKernelDemo.Services.Plugins;

public class MetaDeVendasDoDiaPlugin
{
    [KernelFunction]
    [Description("Obtém a meta de vendas do dia")]
    [return: Description("Meta de vendas do dia de hojeon.")]
    public Target ObterMetaDeVendasDoDia()
    {
        return new Target(10000, 9600);
    }
}
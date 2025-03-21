using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.Ollama;
using OllamaSharp;
using SemanticKernelApp.Services;

namespace SemanticKernelApp;

public static class DependencyInjections
{
    public static IServiceCollection AddSemanticKernelSettings(this IServiceCollection services)
    {
        services.AddOllamaChatCompletion(modelId: "phi3");
        services.AddKernel();

        services.AddSingleton<PromptExecutionSettings>(_ => new OpenAIPromptExecutionSettings
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        });
        services.AddSingleton<AgentService>();
        
        return services;
    }
}
using ChromaDB.Client;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OllamaSharp;
using SemanticKernelDemo.Services;

namespace SemanticKernelApp;

public static class DependencyInjections
{

    public static IServiceCollection AddSemanticKernelSettings(this IServiceCollection services)
    {
        //services.AddOpenAIChatCompletion(OpenAIModelName, OpenAISecret);
        services.AddOllamaChatCompletion(new OllamaApiClient(new Uri("http://localhost:11434/"), "llama3.2"));
        services.AddKernel();

        // services.AddSingleton<PromptExecutionSettings>(_ => new OpenAIPromptExecutionSettings
        // {
        //     ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        // });
        services.AddSingleton<PromptExecutionSettings>(_ => new OllamaPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
        });
        services.AddSingleton<AgentService>();
        
        return services;
    }
}
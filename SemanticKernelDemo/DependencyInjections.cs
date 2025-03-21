using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelDemo.Services;

namespace SemanticKernelApp;

public static class DependencyInjections
{
    #region Secrets
    private const string OpenAISecret =
        "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";
    #endregion

    private const string OpenAIModelName = "gpt-4o-mini";
    
    public static IServiceCollection AddSemanticKernelSettings(this IServiceCollection services)
    {
        //services.AddOpenAIChatCompletion(OpenAIModelName, OpenAISecret);
        services.AddOllamaChatCompletion("phi3");
        services.AddKernel();

        // services.AddSingleton<PromptExecutionSettings>(_ => new OpenAIPromptExecutionSettings
        // {
        //     ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
        // });
        services.AddSingleton<PromptExecutionSettings>(_ => new OllamaPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        });
        services.AddSingleton<AgentService>();
        
        return services;
    }
}
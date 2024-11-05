using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using SemanticKernelDemo.Services;

namespace SemanticKernelDemo;

public static class DependencyInjections
{
    #region Secrets
    private const string OpenAISecret =
        "...";
    #endregion

    private const string OpenAIModelName = "gpt-4o-mini";
    
    public static void AddSemanticKernelSettings(this IServiceCollection services)
    {
        if (string.IsNullOrEmpty(OpenAISecret))
            throw new Exception("Informe a chave do Open AI");
        
        services.AddOpenAIChatCompletion(OpenAIModelName, OpenAISecret);
        services.AddKernel();
        
        services.AddSingleton<PromptExecutionSettings>(_ => new OpenAIPromptExecutionSettings
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            //ResponseFormat = AssistantResponseFormat.CreateJsonObjectFormat()
        });
        services.AddSingleton<AgentService>();
    }
}
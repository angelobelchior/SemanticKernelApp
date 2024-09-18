using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace SemanticKernelApp.Services;

public class AgentService
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly Kernel _kernel;
    private readonly PromptExecutionSettings _promptExecutionSettings;
    private readonly ChatHistory history = new();

    public AgentService(
        IChatCompletionService chatCompletionService, 
        Kernel kernel,
        PromptExecutionSettings promptExecutionSettings)
    {
        _chatCompletionService = chatCompletionService;
        _kernel = kernel;
        _promptExecutionSettings = promptExecutionSettings;

        _kernel.ImportPluginFromType<UserInfoKernel>();
    }

    public async Task<string> SendMessageAsync(string message)
    {
        try
        {
            history.AddUserMessage(message);
            
            var response = await _chatCompletionService.GetChatMessageContentAsync(history, _promptExecutionSettings, _kernel);
            history.Add(response);
            
            return response.Content ?? string.Empty;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}

public class UserInfoKernel
{
    [KernelFunction]
    [Description("Obtém a idade de uma determinada pessoa")]
    public int ObtemIdadeDaPessoa(string nome)
    {
        if (nome == "Angelo") return 40;

        return 0;
    }
}
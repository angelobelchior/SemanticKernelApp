using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelApp.Models;

namespace SemanticKernelApp.Services;

public class AgentService
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly Kernel _kernel;
    private readonly PromptExecutionSettings _promptExecutionSettings;
    private readonly ChatHistory _history = new();

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
    
    public async Task InitializeUserMessageAsync(User user)
    {
        try
        {
            var message = user.ToString();
            _history.AddUserMessage(message);
            
            var response = await _chatCompletionService.GetChatMessageContentAsync(_history, _promptExecutionSettings, _kernel);
            _history.Add(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<string> SendMessageAsync(string message)
    {
        try
        {
            _history.AddUserMessage(message);
            
            var response = await _chatCompletionService.GetChatMessageContentAsync(_history, _promptExecutionSettings, _kernel);
            _history.Add(response);
            
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
    [Description("Obtém as vendas do dia feitas pelo usuário")]
    public string GetSalesOfTheDay(int id)
    {
        var sales = 
$"""
    Vendas efetuadas pelo usuário {id}:
    PS5 - R$ 5000,00
    XBOX - R$ 4000,00
    God of War - R$ 350,00
    The Last of Us - R$ 250,00
""";
        
        return sales;
    }
}
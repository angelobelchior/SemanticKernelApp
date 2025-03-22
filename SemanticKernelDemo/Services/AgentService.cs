using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using SemanticKernelDemo.Models;
using SemanticKernelDemo.Services.Plugins;

namespace SemanticKernelDemo.Services;

public class AgentService
{
    private readonly IChatCompletionService _chatCompletionService;
    private readonly Kernel _kernel;
    private readonly PromptExecutionSettings _promptExecutionSettings;
    private readonly ChatHistory _history = new();

    private const string _defaultPromptTemplate = 
"""

"""
;

    public AgentService(
        IChatCompletionService chatCompletionService, 
        Kernel kernel,
        PromptExecutionSettings promptExecutionSettings)
    {
        _chatCompletionService = chatCompletionService;
        _kernel = kernel;
        _promptExecutionSettings = promptExecutionSettings;

        _kernel.ImportPluginFromType<VendasDoDiaPlugin>();
        _kernel.ImportPluginFromType<MetaDeVendasDoDiaPlugin>();
        _kernel.ImportPluginFromType<SolicitacaoDeReembolsoPlugin>();
    }
    
    public async Task InitializeUserMessageAsync(User user)
    {
        try
        {
            var userMessage = user.ToString();
            var message = userMessage + _defaultPromptTemplate;
            _history.AddUserMessage(message);
            
            var response = await _chatCompletionService.GetChatMessageContentAsync(_history, _promptExecutionSettings, _kernel);
            _history.Add(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<string> SendMessageAsync(string userMessage)
    {
        try
        {
            var message = userMessage + _defaultPromptTemplate;
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
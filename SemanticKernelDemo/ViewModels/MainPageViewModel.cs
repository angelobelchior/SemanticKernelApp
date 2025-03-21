using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SemanticKernelDemo.Models;
using SemanticKernelDemo.Services;

namespace SemanticKernelDemo.ViewModels;

public partial class MainPageViewModel(AgentService agentService) : ObservableObject
{
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(SemanticKernelDemo.ViewModels.MainPageViewModel.SendMessageCommand))]
    private string _text = string.Empty;

    public ObservableCollection<Message> Messages { get; set; } = [];

    public async Task OnLoadingAsync()
    {
        Messages.Add(Message.FromAgent("Olá, como posso te ajudar?"));
        
        await agentService.InitializeUserMessageAsync(new(1, "Angelo", 40, "Vendedor"));
    }

    [RelayCommand(CanExecute = nameof(CanSendMessage))]
    private async Task SendMessageAsync()
    {
        Messages.Add(Message.FromUser(Text, "Angelo"));
        var text = await agentService.SendMessageAsync(Text);

        Messages.Add(Message.FromAgent(text));

        Text = string.Empty;
    }

    private bool CanSendMessage() => !string.IsNullOrWhiteSpace(Text);
}
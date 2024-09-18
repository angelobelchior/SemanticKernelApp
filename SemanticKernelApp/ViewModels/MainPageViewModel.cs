using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SemanticKernelApp.Models;
using SemanticKernelApp.Services;

namespace SemanticKernelApp.ViewModels;

public partial class MainPageViewModel(AgentService agentService) : ObservableObject
{

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SendMessageCommand))]
    private string _text = string.Empty;


    public ObservableCollection<Message> Messages { get; set; } = [];
    
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
using SemanticKernelApp.ViewModels;

namespace SemanticKernelApp;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel viewModel)
    {
        _viewModel = viewModel;
        
        InitializeComponent();
        BindingContext = _viewModel;
        
        _viewModel.Messages.CollectionChanged += (_, _) =>
        {
            var message = _viewModel.Messages.Last();
            Messages.ScrollTo(message, ScrollToPosition.End, true);
            Message.Focus();
        };
    }

    protected override async void OnAppearing()
        => await _viewModel.OnLoadingAsync();
}
using SemanticKernelDemo.ViewModels;

namespace SemanticKernelDemo;

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

    private bool _onAppearing = false;

    protected override async void OnAppearing()
    {
        if (_onAppearing) return;
        _onAppearing = true;
        await _viewModel.OnLoadingAsync();
    }
}
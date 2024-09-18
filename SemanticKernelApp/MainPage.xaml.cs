using SemanticKernelApp.ViewModels;

namespace SemanticKernelApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        
        viewModel.Messages.CollectionChanged += (_, _) =>
        {
            var message = viewModel.Messages.Last();
            listView.ScrollTo(message, ScrollToPosition.End, true);
        };
    }
}
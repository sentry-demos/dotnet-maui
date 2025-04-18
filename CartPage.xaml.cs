namespace DotNetMaui;

public partial class CartPage : ContentPage
{
    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ((CartViewModel)BindingContext).LoadCommand.Execute(null!);
    }
}
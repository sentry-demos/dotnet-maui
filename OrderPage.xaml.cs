namespace DotNetMaui;

public partial class OrderPage : ContentPage
{
    public OrderPage(OrderViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        ((OrderViewModel)BindingContext).LoadCommand.Execute(null!);
    }
}
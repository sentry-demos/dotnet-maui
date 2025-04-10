namespace DotNetMaui;

public partial class CartViewModel : ObservableObject
{
    [RelayCommand]
    async Task Load()
    {
        
    }

    [RelayCommand]
    Task Order() => Shell.Current.GoToAsync("OrderPage");
}
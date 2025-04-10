namespace DotNetMaui;

public partial class ListViewModel : ObservableObject
{
    [RelayCommand]
    async Task Load()
    {
        
    }
    
    
    [RelayCommand]
    Task ViewCart() => Shell.Current.GoToAsync("CartPage");
}
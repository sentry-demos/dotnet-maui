using DotNetMaui.Services;
using Empower.Models;

namespace DotNetMaui;

public partial class CartViewModel(IDataService dataService) : ObservableObject
{
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(CartCount))]
    [NotifyPropertyChangedFor(nameof(SubTotal))]
    List<Product> cart;
    
    public int CartCount => Cart?.Count ?? 0;
    public int SubTotal => Cart?.Sum(x => x.Price) ?? 0;
    
    
    [RelayCommand]
    async Task Load() => this.Cart = await dataService.GetCartItems();

    [RelayCommand]
    async Task Remove(Product product)
    {
        await dataService.RemoveFromCart(product);
        this.LoadCommand.Execute(null!);
    }
    
    [RelayCommand]
    Task Order() => Shell.Current.GoToAsync("OrderPage");
}
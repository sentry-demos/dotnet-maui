using EmpowerPlant.Services;
using Empower.Models;

namespace EmpowerPlant;

public partial class CartViewModel(IDataService dataService) : ObservableObject
{
    [ObservableProperty] 
    [NotifyPropertyChangedFor(nameof(CartCount))]
    [NotifyPropertyChangedFor(nameof(SubTotal))]
    [NotifyCanExecuteChangedFor(nameof(OrderCommand))]
    List<Product> cart = new();
    
    public int CartCount => Cart.Count;
    public int SubTotal => Cart.Sum(x => x.Price);
    
    
    [RelayCommand]
    async Task Load() => this.Cart = await dataService.GetCartItems();

    [RelayCommand]
    async Task Remove(Product product)
    {
        await dataService.RemoveFromCart(product);
        this.LoadCommand.Execute(null!);
    }
    
    [RelayCommand(CanExecute = nameof(CanOrder))]
    Task Order() => Shell.Current.GoToAsync("OrderPage");
    bool CanOrder() => this.CartCount > 0;
}
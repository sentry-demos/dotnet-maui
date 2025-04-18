using DotNetMaui.Services;
using Empower.Models;

namespace DotNetMaui;

public partial class ListViewModel(IDataService dataService) : ObservableObject
{
    [ObservableProperty] List<Product> products;

    [RelayCommand]
    async Task Load() => this.Products = await dataService.GetProducts();

    [RelayCommand]
    async Task AddToCart(Product product) => await dataService.AddToCart(product);
    
    [RelayCommand]
    Task ViewCart() => Shell.Current.GoToAsync("CartPage");
}
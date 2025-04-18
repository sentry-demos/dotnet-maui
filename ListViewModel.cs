using DotNetMaui.Services;
using Empower.Models;

namespace DotNetMaui;

public partial class ListViewModel(IDataService dataService) : ObservableObject
{
    [ObservableProperty] List<Product> products;
    [ObservableProperty] bool isBusy;

    [RelayCommand]
    async Task Load()
    {
        this.IsBusy = true;
        this.Products = await dataService.GetProducts();
        this.IsBusy = false;
    }

    [RelayCommand]
    async Task AddToCart(Product product)
    {
        await dataService.AddToCart(product);
        await Shell.Current.DisplayAlert(null, "Added to Cart", "OK");
    }

    [RelayCommand]
    Task ViewCart() => Shell.Current.GoToAsync("CartPage");
}
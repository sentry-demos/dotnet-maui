using EmpowerPlant.Services;
using EmpowerPlant.Models;

namespace EmpowerPlant;

public partial class ListViewModel(IDataService dataService) : ObservableObject
{
    [ObservableProperty] List<Product> products;

    // we should safety these calls to prevent network blips from crashing our app, but Sentry has got us for reporting if it does
    [RelayCommand]
    async Task Load() => this.Products = await dataService.GetProducts();

    [RelayCommand]
    async Task AddToCart(Product product)
    {
        await dataService.AddToCart(product);
        await Shell.Current.DisplayAlert(null, "Added to Cart", "OK");
    }

    [RelayCommand]
    Task ViewCart() => Shell.Current.GoToAsync("CartPage");
}
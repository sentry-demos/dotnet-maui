using DotNetMaui.Services;
using Empower.Models;
using Microsoft.Extensions.Logging;

namespace DotNetMaui;

public partial class OrderViewModel(
    ILogger<OrderViewModel> logger,
    IDataService dataService
) : ObservableObject
{
    List<Product> cart;
    
    [ObservableProperty] string email;

    [RelayCommand]
    async Task Load()
    {
        this.cart = await dataService.GetCartItems();
    }
    
    [RelayCommand]
    async Task PlaceOrder()
    {
        try
        {
            await dataService.Checkout();
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            // exception data goes to Sentry
            var subTotal = this.cart.Sum(x => x.Price);
            ex.Data.Add("LostRevenue", subTotal);
            
            // this logger, while not sentry specific is completely plugged into Sentry as well
            logger.LogError(ex, "Error");

            await Shell.Current.DisplayAlert("Error", "There was an error processing your order", "Ok");
        }
    }
}
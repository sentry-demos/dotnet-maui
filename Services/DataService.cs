using System.Net.Http.Json;
using Empower.Models;

namespace DotNetMaui.Services;

public interface IDataService
{
    Task Checkout();

    // no quantity - Bruno didn't like data models on the server :)
    Task AddToCart(Product item);
    Task RemoveFromCart(Product item);
    Task<List<Product>> GetCartItems();
    Task<List<Product>> GetProducts();
}

public class DataService : IDataService
{
    readonly HttpClient httpClient = new(new SentryHttpMessageHandler())
    {
        BaseAddress = new(Configuration.BackendUrl)
    };
    readonly List<Product> cart = new();
    

    public async Task Checkout()
    {
        await this.httpClient.PostAsync("/checkout", new StringContent(String.Empty)).ConfigureAwait(false);
        this.cart.Clear();
    }

    public Task AddToCart(Product item)
    {
        if (!this.cart.Any(x => x.Id == item.Id))
            this.cart.Add(item);
        
        return Task.CompletedTask;
    }

    public Task RemoveFromCart(Product item)
    {
        var cartItem = this.cart.SingleOrDefault(x => x.Id == item.Id);
        if (cartItem != null)
            this.cart.Remove(cartItem);
        
        return Task.CompletedTask;
    }

    public Task<List<Product>> GetCartItems() => Task.FromResult(this.cart);

    public Task<List<Product>> GetProducts()
        => this.httpClient.GetFromJsonAsync<List<Product>>("/products")!;
}
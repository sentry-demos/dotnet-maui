using System.Net.Http.Json;
using Empower.Models;
using Microsoft.Extensions.Logging;

namespace DotNetMaui.Services;

public interface IDataService
{
    Task Checkout();
    Task AddToCart(Product item);
    Task RemoveFromCart(Product item);
    Task<List<Product>> GetCartItems();
    Task<List<Product>> GetProducts();
}

public class DataService(ILogger<DataService> logger) : IDataService
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
        ArgumentNullException.ThrowIfNull(item);
        if (!this.cart.Any(x => x.Id == item.Id))
            this.cart.Add(item);
        
        // Informational logs become Sentry breadcrumbs automatically.
        logger.LogInformation("Adding product to cart: {productId} - {productName}", item.Id, item.Description);
        return Task.CompletedTask;
    }

    public Task RemoveFromCart(Product item)
    {
        ArgumentNullException.ThrowIfNull(item);
        
        var cartItem = this.cart.SingleOrDefault(x => x.Id == item.Id);
        if (cartItem != null)
            this.cart.Remove(cartItem);
        
        // Informational logs become Sentry breadcrumbs automatically.
        logger.LogInformation("Adding product to cart: {productId} - {productName}", item.Id, item.Description);
        
        return Task.CompletedTask;
    }

    public Task<List<Product>> GetCartItems() => Task.FromResult(this.cart.ToList()); // cart externally should be imm

    public Task<List<Product>> GetProducts()
        => this.httpClient.GetFromJsonAsync<List<Product>>("/products")!;
}
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

// <summary>
// CachedBasketRepository is a wrapper around IBasketRepository that provides caching functionality.
// It implements the IBasketRepository interface and delegates calls to the underlying repository.
// Acts like a proxy for the actual repository, allowing for caching or other cross-cutting concerns to be applied.
// </summary>
public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{
    public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);

        if (!string.IsNullOrEmpty(cacheBasket))
        {
            return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
        }

        var basket = await repository.GetBasketAsync(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

        return basket;
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        await repository.StoreBasketAsync(cart, cancellationToken);
        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);

        return cart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    { 
        await repository.DeleteBasketAsync(userName, cancellationToken);
        await cache.RemoveAsync(userName, cancellationToken);
        
        return true;
    }
}
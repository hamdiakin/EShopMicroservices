using Marten.Schema;

namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    
    public ShoppingCart(string userName)
    {
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
    
    public ShoppingCart() { }
}
namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid productId)
        : base($"Product with ID {productId} not found.")
    {
    }
}
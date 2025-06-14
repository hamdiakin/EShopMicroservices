namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        
        logger.LogInformation("GetProductByIdHandler: Handling GetProductByIdQuery @{query}", query);
        
        // Retrieve the product by ID from the database
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        
        if (product is null)
        {
            logger.LogWarning("Product with ID {Id} not found", query.Id);
            throw new ProductNotFoundException(query.Id);
        }
        
        // Return the result
        return new GetProductByIdResult(product);
    }
}
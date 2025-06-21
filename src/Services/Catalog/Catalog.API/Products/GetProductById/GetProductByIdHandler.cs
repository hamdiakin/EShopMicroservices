namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler (IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        // Retrieve the product by ID from the database
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        
        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        
        // Return the result
        return new GetProductByIdResult(product);
    }
}
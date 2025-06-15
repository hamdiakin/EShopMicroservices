namespace Catalog.API.Products.GetProducts;

// public record GetProductsRequest();
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            var query = new GetProductsQuery();
            var result = await sender.Send(query);
            return Results.Ok(result.Adapt<GetProductsResponse>());
        })
        .WithName("Get Products")
        .WithSummary("Retrieves all products")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
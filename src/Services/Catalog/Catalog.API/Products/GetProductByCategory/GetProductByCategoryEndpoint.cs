namespace Catalog.API.Products.GetProductByCategory;

// public record GetProductByCatergoryRequst()
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, 
            ISender sender) =>
        {
            var query = new GetProductByCategoryQuery(category);
            var result = await sender.Send(query);
            
            return Results.Ok(new GetProductByCategoryResponse(result.Products));
        })
        .WithName("GetProductByCategory")
        .WithSummary("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>()
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces(404);
    }
}
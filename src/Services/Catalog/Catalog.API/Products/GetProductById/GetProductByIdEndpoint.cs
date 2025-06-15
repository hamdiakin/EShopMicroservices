namespace Catalog.API.Products.GetProductById;

// public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetProductByIdQuery(id);
            var result = await sender.Send(query);
            return Results.Ok(result.Adapt<GetProductByIdResponse>());
        })
        .WithName("Get Product By Id")
        .WithSummary("Retrieves a product by its ID")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}
namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductRequest(Guid ProductId);
public record DeleteProductResponse(bool IsSuccess, string ErrorMessage = "");

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{productId:guid}", async (Guid productId, ISender sender) =>
        {
            var command = new DeleteProductCommand(productId);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteProductResponse>();
            
            if (result.IsSuccess)
            {
                return Results.Ok(response);
            }

            return Results.BadRequest(new DeleteProductResponse(false, result.ErrorMessage));
        })
        .WithName("DeleteProduct")
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .Produces<DeleteProductResponse>(StatusCodes.Status400BadRequest);
    }
}
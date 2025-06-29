namespace Basket.API.Basket.DeleteBasket;

// public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteBasketResponse>();
                return result.IsSuccess ? Results.Ok(response) : Results.NotFound(response);
            })
            .WithName("DeleteBasket")
            .WithSummary("Delete a user's shopping basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .Produces<DeleteBasketResponse>(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Basket");
    }
}
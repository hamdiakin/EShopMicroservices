namespace Basket.API.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBaskerCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBaskerCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull();
        RuleFor(x => x.Cart.UserName).NotEmpty();
        // RuleFor(x => x.Cart.Items).NotEmpty();
    }
}

public class StoreBasketCommandHandler (IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.StoreBasketAsync(command.Cart, cancellationToken);
        
        return new StoreBasketResult(command.Cart.UserName);
    }
}
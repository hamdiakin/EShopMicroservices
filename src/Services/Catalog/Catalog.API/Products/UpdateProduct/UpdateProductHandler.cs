namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string? Description,
    string Image,
    decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        RuleFor(x => x.Category)
            .NotNull().WithMessage("Category is required.")
            .Must(c => c != null && c.Count > 0).WithMessage("At least one category is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}
internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.Image = command.Image;
        product.Price = command.Price;

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
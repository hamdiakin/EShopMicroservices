namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess, string ErrorMessage = "");

internal class DeleteProductCommandHandler (IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling DeleteProductCommand for ProductId: {ProductId}", command.ProductId);

        var product = await session.LoadAsync<Product>(command.ProductId, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with ID {ProductId} not found", command.ProductId);
            return new DeleteProductResult(false, "Product not found");
        }

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Product with ID {ProductId} deleted successfully", command.ProductId);
        return new DeleteProductResult(true);
    }
}
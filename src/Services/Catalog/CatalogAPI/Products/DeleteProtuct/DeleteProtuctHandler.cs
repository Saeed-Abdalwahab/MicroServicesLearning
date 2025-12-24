
using CatalogAPI.Products.UpdateProduct;

namespace CatalogAPI.Products.DeleteProtuct
{
    public record DeleteProtuctCommand(Guid Id) : ICommand<DeleteProtuctResult>;
    public record DeleteProtuctResult(bool IsSuccess);
    internal class DeleteProtuctCommandHandler(IDocumentSession session, ILogger<DeleteProtuctCommandHandler> logger)
        : ICommandHandler<DeleteProtuctCommand, DeleteProtuctResult>
    {
        public async Task<DeleteProtuctResult> Handle(DeleteProtuctCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DeleteProtuctCommand with {@Command}", command);
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null)
            {
                logger.LogWarning("Product with Id {ProductId} not found.", command.Id);
                throw new ProductNotFoundException();
            }
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Product with Id {ProductId} deleted successfully.", command.Id);
            return new DeleteProtuctResult(true);
        }
    }
}

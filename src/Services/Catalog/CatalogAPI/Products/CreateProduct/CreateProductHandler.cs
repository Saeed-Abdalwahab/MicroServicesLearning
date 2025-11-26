
namespace CatalogAPI.Products.CreateProduct
{
    public record CreateProductCommand   (string Name, string Description, string ImageFile, List<string> Category, decimal Price)
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var prodcut = new Product()
            {
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Name = command.Name,
                Price = command.Price

            };
            session.Store(prodcut);
           await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(prodcut.Id);
        }

    }
}

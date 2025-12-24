


namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session,ILogger<GetProductByIdQuery> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
           var product =await session.LoadAsync<Product>(query.Id,cancellationToken);
              if(product is  null)
              {
                logger.LogWarning("Product with Id {ProductId} not found",query.Id);
                throw new ProductNotFoundException ();
            } 
              return new GetProductByIdResult(product);
        }
    }
}

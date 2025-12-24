
namespace CatalogAPI.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler(IDocumentSession session,ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
             logger.LogInformation("Handling GetProductByCategoryQuery with {@Query}", query);
            var products =await session.Query<Product>()
                                  .Where(p => p.Category.Contains(query.Category))
                                  .ToListAsync();
            var result = new GetProductByCategoryResult(products);
            return result;
        }
    }
}


namespace CatalogAPI.Products.GetProductByCategory
{

    //public record GetProductByCategoryRequest();
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var result = await sender.Send(query, cancellationToken);
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductByCategory")
            .WithTags("Products")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithDescription("Gets products by category.");
        }
    }
}

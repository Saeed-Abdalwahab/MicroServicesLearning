
namespace CatalogAPI.Products.GetProducts
{
    public record GetProductsRequest() : IQuery<GetProductsResult>;
    public record GetProductsResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender, CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetProductsQuery(), cancellationToken);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products")
            .WithDescription("Retrieves a list of all products in the catalog.");
        }
    }
}

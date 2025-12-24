
namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, string Description, string ImageFile, List<string> Category, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products ", async (  UpdateProductRequest request, ISender sender, CancellationToken cancellationToken) =>
            {
               
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command, cancellationToken);
                var response = new UpdateProductResponse(result.IsSuccess);
                if (result.IsSuccess)
                {
                    return Results.Ok(response);
                }
                else
                {
                    return Results.NotFound("Product not found.");
                }
            })
            .WithName("UpdateProduct")
            .WithTags("Products")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Updates an existing product in the catalog.");


        }
    }
}

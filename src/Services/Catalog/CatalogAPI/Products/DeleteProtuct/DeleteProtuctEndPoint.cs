
namespace CatalogAPI.Products.DeleteProtuct
{
    public record DeleteProtuctRequest(Guid Id);
    public record DeleteProtuctResponse(bool IsSuccess);
    public class DeleteProtuctEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
            {
                var command = new DeleteProtuctCommand(id);
                var result = await sender.Send(command, cancellationToken);
                var response = new DeleteProtuctResponse(result.IsSuccess);
                if (result.IsSuccess)
                {
                    return Results.Ok(response);
                }
                else
                {
                    return Results.NotFound("Product not found.");
                }
            }).WithName("DeleteProduct")
             .WithTags("Products")
             .Produces<DeleteProtuctResponse>(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status404NotFound)
             .WithDescription("Deletes an existing product from the catalog.");
        }
    }
}

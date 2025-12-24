namespace CatalogAPI.Products.GetProductById
{
    //public record GetProductByIdRequest(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndPoint:ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                //var query = new GetProductByIdRequest(id);
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Gets a product by Id")
            .WithDescription("Gets a product by its unique identifier from the catalog.");
        }
    }
 
}

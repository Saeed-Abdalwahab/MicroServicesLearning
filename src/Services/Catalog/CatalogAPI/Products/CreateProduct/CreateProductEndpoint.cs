using BuildingBlocks.CQRS;

namespace CatalogAPI.Products.CreateProduct
{
    public record  CreateProductRequest(string Name, string Description, string ImageFile, List<string> Category, decimal Price)
        :  ICommand<CreateProductResult>;

    public record  CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command =request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{result.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Creates a new product")
            .WithDescription("Creates a new product in the catalog.");
            ;
        }
    }
}

using CatalogAPI.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCarterAutoDiscovery([typeof(Program).Assembly]);
 builder.Services.AddMarten(options =>
 {
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
  });

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly (typeof(Program).Assembly));

var app = builder.Build();
// Configure the HTTP request pipeline.

app.MapCarter();

app.Run();

using e_commerce_web_app_server;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB client and services
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB");
    return new MongoClient(connectionString);
});
builder.Services.AddScoped<MongoDBProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "hello world");

app.MapGet("/products", async (MongoDBProductService service) =>
{
    try
    {
        var products = await service.GetProductsAsync();
        return Results.Ok(products);
    }
    catch (Exception)
    {
        Console.WriteLine("Error retrieving products");
        throw;
    }
});

app.MapGet("/products/{id}", async (int id, MongoDBProductService service) =>
{
    try
    {
        var product = await service.GetProductByIdAsync(Convert.ToString(id));
        if (product == null)
        {
            return Results.NotFound("Product not found");
        }
        return Results.Ok(product);
    }
    catch (Exception)
    {
        Console.WriteLine("Error retrieving product");
        throw;
    }
});

app.MapPost("/products", async (Product product, MongoDBProductService service) =>
{
    try
    {
        await service.CreateProductAsync(product);
        return Results.Created($"/products/{product.Id}", product);
    }
    catch (Exception)
    {
        Console.WriteLine("Error creating product");
        throw;
    }
});

app.MapPut("/products/{id}", async (int id, Product updatedProduct, MongoDBProductService service) =>
{
    try
    {
        var existingProduct = await service.GetProductByIdAsync(Convert.ToString(id));
        if (existingProduct == null)
        {
            return Results.NotFound("Product not found");
        }

        await service.UpdateProductAsync(Convert.ToString(id), updatedProduct);
        return Results.Ok(updatedProduct);
    }
    catch (Exception)
    {
        Console.WriteLine("Error updating product");
        throw;
    }
});

app.MapDelete("/products/{id}", async (int id, MongoDBProductService service) =>
{
    try
    {
        var existingProduct = await service.GetProductByIdAsync(Convert.ToString(id));
        if (existingProduct == null)
        {
            return Results.NotFound("Product not found");
        }

        await service.DeleteProductAsync(Convert.ToString(id));
        return Results.Ok("Product deleted successfully");
    }
    catch (Exception)
    {
        Console.WriteLine("Error deleting product");
        throw;
    }
});

app.Run();
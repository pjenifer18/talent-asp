using System;
using Talent.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
namespace Talent.Controllers
{
	public class ProductScalfController
	{
		public ProductScalfController()
		{
		}
	}


public static class ProductModelEndpoints
{
	public static void MapProductModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/ProductModel", async (DBContext db) =>
        {
            return await db.Products.ToListAsync();
        })
        .WithName("GetAllProductModels")
        .Produces<List<ProductModel>>(StatusCodes.Status200OK);

        routes.MapGet("/api/ProductModel/{id}", async (Guid ProductId, DBContext db) =>
        {
            return await db.Products.FindAsync(ProductId)
                is ProductModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetProductModelById")
        .Produces<ProductModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/ProductModel/{id}", async (Guid ProductId, ProductModel productModel, DBContext db) =>
        {
            var foundModel = await db.Products.FindAsync(ProductId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(productModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateProductModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/ProductModel/", async (ProductModel productModel, DBContext db) =>
        {
            db.Products.Add(productModel);
            await db.SaveChangesAsync();
            return Results.Created($"/ProductModels/{productModel.ProductId}", productModel);
        })
        .WithName("CreateProductModel")
        .Produces<ProductModel>(StatusCodes.Status201Created);


        routes.MapDelete("/api/ProductModel/{id}", async (Guid ProductId, DBContext db) =>
        {
            if (await db.Products.FindAsync(ProductId) is ProductModel productModel)
            {
                db.Products.Remove(productModel);
                await db.SaveChangesAsync();
                return Results.Ok(productModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteProductModel")
        .Produces<ProductModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}


using System;
using Talent.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
namespace Talent.Controllers
{
	public class SalesScalfController
	{
		public SalesScalfController()
		{
		}
	}


public static class SaleModelEndpoints
{
	public static void MapSaleModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/SaleModel", async (DBContext db) =>
        {
            return await db.Sales.ToListAsync();
        })
        .WithName("GetAllSaleModels")
        .Produces<List<SaleModel>>(StatusCodes.Status200OK);

        routes.MapGet("/api/SaleModel/{id}", async (Guid SaleId, DBContext db) =>
        {
            return await db.Sales.FindAsync(SaleId)
                is SaleModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetSaleModelById")
        .Produces<SaleModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/SaleModel/{id}", async (Guid SaleId, SaleModel saleModel, DBContext db) =>
        {
            var foundModel = await db.Sales.FindAsync(SaleId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(saleModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateSaleModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/SaleModel/", async (SaleModel saleModel, DBContext db) =>
        {
            db.Sales.Add(saleModel);
            await db.SaveChangesAsync();
            return Results.Created($"/SaleModels/{saleModel.SaleId}", saleModel);
        })
        .WithName("CreateSaleModel")
        .Produces<SaleModel>(StatusCodes.Status201Created);


        routes.MapDelete("/api/SaleModel/{id}", async (Guid SaleId, DBContext db) =>
        {
            if (await db.Sales.FindAsync(SaleId) is SaleModel saleModel)
            {
                db.Sales.Remove(saleModel);
                await db.SaveChangesAsync();
                return Results.Ok(saleModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteSaleModel")
        .Produces<SaleModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}


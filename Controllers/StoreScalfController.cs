using System;
using Talent.Data;
using Microsoft.EntityFrameworkCore;
using Talent.Models.Domain;
namespace Talent.Controllers
{
	public class StoreScalfController
	{
		public StoreScalfController()
		{
		}
	}


public static class StoreModelEndpoints
{
	public static void MapStoreModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/StoreModel", async (DBContext db) =>
        {
            return await db.Stores.ToListAsync();
        })
        .WithName("GetAllStoreModels")
        .Produces<List<StoreModel>>(StatusCodes.Status200OK);

        routes.MapGet("/api/StoreModel/{id}", async (Guid StoreId, DBContext db) =>
        {
            return await db.Stores.FindAsync(StoreId)
                is StoreModel model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetStoreModelById")
        .Produces<StoreModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/StoreModel/{id}", async (Guid StoreId, StoreModel storeModel, DBContext db) =>
        {
            var foundModel = await db.Stores.FindAsync(StoreId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(storeModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateStoreModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/StoreModel/", async (StoreModel storeModel, DBContext db) =>
        {
            db.Stores.Add(storeModel);
            await db.SaveChangesAsync();
            return Results.Created($"/StoreModels/{storeModel.StoreId}", storeModel);
        })
        .WithName("CreateStoreModel")
        .Produces<StoreModel>(StatusCodes.Status201Created);


        routes.MapDelete("/api/StoreModel/{id}", async (Guid StoreId, DBContext db) =>
        {
            if (await db.Stores.FindAsync(StoreId) is StoreModel storeModel)
            {
                db.Stores.Remove(storeModel);
                await db.SaveChangesAsync();
                return Results.Ok(storeModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteStoreModel")
        .Produces<StoreModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}


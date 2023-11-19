using System;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Models.Domain;
using Microsoft.Extensions.Logging;
namespace Talent.Controllers
{
	public class CustomerScalfController
	{
		public CustomerScalfController()
		{
		}
	}


public static class CustomerModelEndpoints
{
	public static void MapCustomerModelEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/CustomerModel", async (DBContext db) =>
        {
            return await db.Customers.ToListAsync();
        })
        .WithName("GetAllCustomerModels")
        .Produces<List<CustomerModel>>(StatusCodes.Status200OK);

            routes.MapGet("/api/CustomerModel/{id}", async (Guid CustomerId, DBContext db) =>
            {
                return await db.Customers.FindAsync(CustomerId)
                    is CustomerModel model
                        ? Results.Ok(model)
                        : Results.NotFound();
            })
            .WithName("GetCustomerModelById")
            .Produces<CustomerModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/CustomerModel/{id}", async (Guid CustomerId, CustomerModel customerModel, DBContext db) =>
        {
            var foundModel = await db.Customers.FindAsync(CustomerId);

            if (foundModel is null)
            {
                return Results.NotFound();
            }
            customerModel.CustomerId = foundModel.CustomerId;
            db.Update(customerModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateCustomerModel")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/CustomerModel/", async (CustomerModel customerModel, DBContext db) =>
        {
            db.Customers.Add(customerModel);
            await db.SaveChangesAsync();
            return Results.Created($"/CustomerModels/{customerModel.CustomerId}", customerModel);
        })
        .WithName("CreateCustomerModel")
        .Produces<CustomerModel>(StatusCodes.Status201Created);


        routes.MapDelete("/api/CustomerModel/{id}", async (Guid CustomerId, DBContext db) =>
        {
            if (await db.Customers.FindAsync(CustomerId) is CustomerModel customerModel)
            {
                db.Customers.Remove(customerModel);
                await db.SaveChangesAsync();
                return Results.Ok(customerModel);
            }

            return Results.NotFound();
        })
        .WithName("DeleteCustomerModel")
        .Produces<CustomerModel>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}}


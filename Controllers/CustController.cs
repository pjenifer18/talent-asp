using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Talent.Data;
using Talent.Models.Domain;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Talent.Controllers
{
    //[Route("api/[controller]")]
    //public class CustController : Controller
    //{
    //    // GET: api/values
    //    [HttpGet]
    //    public IEnumerable<string> Get()
    //    {
    //        return new string[] { "value1", "value2" };
    //    }

    //    // GET api/values/5
    //    [HttpGet("{id}")]
    //    public string Get(int id)
    //    {
    //        return "value";
    //    }

    //    // POST api/values
    //    [HttpPost]
    //    public void Post([FromBody]string value)
    //    {
    //    }

    //    // PUT api/values/5
    //    [HttpPut("{id}")]
    //    public void Put(int id, [FromBody]string value)
    //    {
    //    }

    //    // DELETE api/values/5
    //    [HttpDelete("{id}")]
    //    public void Delete(int id)
    //    {
    //    }
    //}


    public static class CustModelEndpoints
    {
        public static void MapCustModelEndpoints(this IEndpointRouteBuilder routes)
        {
            //routes.MapGet("/api/CustModel", async (DBContext db) =>
            //{
            //    return await db.CustModel.ToListAsync();
            //})
            //.WithName("GetAllCustModels")
            //.Produces<List<CustModel>>(StatusCodes.Status200OK);

            routes.MapGet("/api/CustModel/{id}", async (Guid CustId, DBContext db) =>
            {
                return await db.CustModel.FindAsync(CustId)
                    is CustModel model
                        ? Results.Ok(model)
                        : Results.NotFound();
            })
            .WithName("GetCustModelById")
            .Produces<CustModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapPut("/api/CustModel/{id}", async (Guid CustId, CustModel custModel, DBContext db) =>
            {
                var foundModel = await db.CustModel.FindAsync(CustId);

                if (foundModel is null)
                {
                    return Results.NotFound();
                }

                db.Update(custModel);

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateCustModel")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

            routes.MapPost("/api/CustModel/", async (CustModel custModel, DBContext db) =>
            {
                db.CustModel.Add(custModel);
                await db.SaveChangesAsync();
                return Results.Created($"/CustModels/{custModel.CustId}", custModel);
            })
            .WithName("CreateCustModel")
            .Produces<CustModel>(StatusCodes.Status201Created);


            routes.MapDelete("/api/CustModel/{id}", async (Guid CustId, DBContext db) =>
            {
                if (await db.CustModel.FindAsync(CustId) is CustModel custModel)
                {
                    db.CustModel.Remove(custModel);
                    await db.SaveChangesAsync();
                    return Results.Ok(custModel);
                }

                return Results.NotFound();
            })
            .WithName("DeleteCustModel")
            .Produces<CustModel>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }

}


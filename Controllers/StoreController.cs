using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Models;
using Talent.Models.Domain;

namespace Talent.Controllers
{
    
    public class StoreController : Controller
    {
        private readonly DBContext dBContext;

        public StoreController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Stores = await dBContext.Stores.ToListAsync();
            return View(Stores);
        }

        [HttpPost]
        public IActionResult Add(AddStoreModel request)
        {
            var Store = new StoreModel()
            {
                StoreId = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address
            };
            dBContext.Stores.Add(Store);
            dBContext.SaveChanges();
            return RedirectToAction("List");
        }
    }
}


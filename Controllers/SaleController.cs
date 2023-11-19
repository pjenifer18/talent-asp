using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Models;
using Talent.Models.Domain;

namespace Talent.Controllers
{
    //[Route("api/Customer")]
    public class SaleController : Controller
	{
		private readonly DBContext dBContext;

		public SaleController( DBContext dBContext )
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
            var Sales = await dBContext.Sales.ToListAsync();

            return View(Sales);

        }

        [HttpPost]
        public IActionResult Add(SaleModel request)
        {
            var Sale = new SaleModel()
            {
                SaleId = Guid.NewGuid(),
                ProductId = request.ProductId,
                CustomerId = request.CustomerId,
                DateSold = request.DateSold
            };
            dBContext.Sales.Add(Sale);
            dBContext.SaveChanges();
            return RedirectToAction("List");
        }

        //[HttpGet]
        //public async Task<IActionResult> View(Guid id)
        //{
        //    var customer = await dBContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

        //    if (customer != null)
        //    {
        //        var viewModel = new UpdateCustomerViewModel()
        //        {
        //            CustomerId = customer.CustomerId,
        //            Name = customer.Name,
        //            Address = customer.Address
        //        };

        //        return await Task.Run (() => View("View", viewModel));
        //    }

        //    return RedirectToAction("List");
        //}

        //[HttpPost]
        //public IActionResult View(UpdateCustomerViewModel updateCustomerViewModel)
        //{
        //    var Customer = dBContext.Customers.Find(updateCustomerViewModel.CustomerId);
        //    if (Customer != null)
        //    {
        //        Customer.Name = updateCustomerViewModel.Name;
        //        Customer.Address = updateCustomerViewModel.Address;
        //        dBContext.SaveChanges();
        //        return RedirectToAction("List");
        //    }
        //    return RedirectToAction("List");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete(UpdateCustomerViewModel updateCustomerViewModel)
        //{
        //    var Customer = await dBContext.Customers.FindAsync(updateCustomerViewModel.ID);
        //    if (Customer != null)
        //    {
        //        dBContext.Customers.Remove(Customer);
        //        await dBContext.SaveChangesAsync();
        //        return dBContext.
        //    }
        //    return RedirectToAction("List");
        //}
    }
}


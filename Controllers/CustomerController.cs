using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Models;
using Talent.Models.Domain;
using Microsoft.Extensions.Logging;

namespace Talent.Controllers
{
    //[Route("api/Customer")]
    public class CustomerController : Controller
	{
		private readonly DBContext dBContext;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController( DBContext dBContext, ILogger<CustomerController> logger)
		{
			this.dBContext = dBContext;
            _logger = logger;
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var Customers = await dBContext.Customers.ToListAsync();
            return View(Customers);
        }

        [HttpPost]
        public IActionResult Add(AddCustomerModel request)
        {
            var Customer = new CustomerModel()
            {
                CustomerId = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address
            };
            dBContext.Customers.Add(Customer);
            dBContext.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var customer = await dBContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            if (customer != null)
            {
                var viewModel = new UpdateCustomerViewModel()
                {
                    CustomerId = customer.CustomerId,
                    Name = customer.Name,
                    Address = customer.Address
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult View(UpdateCustomerViewModel updateCustomerViewModel)
        {
            var Customer = dBContext.Customers.Find(updateCustomerViewModel.CustomerId);
            if (Customer != null)
            {
                Customer.Name = updateCustomerViewModel.Name;
                Customer.Address = updateCustomerViewModel.Address;
                dBContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

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


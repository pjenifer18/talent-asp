using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talent.Data;
using Talent.Models;
using Talent.Models.Domain;

namespace Talent.Controllers
{
	public class ProductController : Controller
	{
		private readonly DBContext dBContext;

		public ProductController(DBContext dBContext)
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
			var Product = await dBContext.Products.ToListAsync();
			return View(Product);
		}

		[HttpPost]
		public IActionResult Add(AddProductModel request)
		{
			var Product = new ProductModel()
			{
				ProductId = Guid.NewGuid(),
				Name = request.Name,
				Price = request.Price
			};

			dBContext.Products.Add(Product);
			dBContext.SaveChanges();
			return RedirectToAction("List");


		}

		[HttpGet]
		public async Task<IActionResult> View(Guid id)
		{
			var product = await dBContext.Products.FirstOrDefaultAsync(x => x.ProductId == id);

			if (product != null)
			{
				var viewModel = new UpdateProductViewModel()
				{
                    ProductId = product.ProductId,
					Name = product.Name,
					Price = product.Price
				};

				return await Task.Run(() => View("View", viewModel));
			}

			return RedirectToAction("List");

		}


		[HttpPost]
		public IActionResult View(UpdateProductViewModel updateProductViewModel)
		{
			var Product = dBContext.Products.Find(updateProductViewModel.ProductId);

			if (Product != null)
			{
				Product.Name = updateProductViewModel.Name;
				Product.Price = updateProductViewModel.Price;
				dBContext.SaveChanges();
				return RedirectToAction("List");
			};

			return RedirectToAction("List");

		}






    }
}


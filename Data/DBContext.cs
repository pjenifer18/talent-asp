using System;
using Microsoft.EntityFrameworkCore;
using Talent.Models.Domain;

namespace Talent.Data
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<CustomerModel> Customers { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<SaleModel> Sales { get; set; }
		public DbSet<StoreModel> Stores { get; set; }
		public DbSet<CustModel> CustModel { get; set; }
	}
}


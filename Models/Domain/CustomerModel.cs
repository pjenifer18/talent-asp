using System;
using System.ComponentModel.DataAnnotations;
namespace Talent.Models.Domain
{
	public class CustomerModel
	{
		[Key]
		public Guid CustomerId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public ICollection<SaleModel> ProductSold { get; set; }
    }
}


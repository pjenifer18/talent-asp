using System;
using System.ComponentModel.DataAnnotations;
namespace Talent.Models.Domain
{
	public class CustModel
	{
		[Key]
		public Guid CustId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        //public ICollection<SaleModel> ProductSold { get; set; }
    }
}


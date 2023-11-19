using System;
namespace Talent.Models
{
	public class UpdateCustomerViewModel
	{
        public Guid CustomerId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
    }
}


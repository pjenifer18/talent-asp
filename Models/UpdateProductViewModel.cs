using System;
namespace Talent.Models
{
	public class UpdateProductViewModel
	{
        public Guid ProductId { get; set; }
        public String Name { get; set; }
        public Double Price { get; set; }
    }
}


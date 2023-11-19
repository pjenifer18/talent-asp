using System;
using System.ComponentModel.DataAnnotations;
namespace Talent.Models.Domain
{
    public class ProductModel
    {
        [Key]
        public Guid ProductId { get; set; }
        public String Name { get; set; }
        public Double Price { get; set; }

        public ICollection<SaleModel> ProductSold { get; set; }
    }
}


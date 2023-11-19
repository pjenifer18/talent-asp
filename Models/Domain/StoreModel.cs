using System;
using System.ComponentModel.DataAnnotations;

namespace Talent.Models.Domain
{
    public class StoreModel
    {
        [Key]
        public Guid StoreId { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }

        public ICollection<SaleModel> ProductSold { get; set; }
    }
}


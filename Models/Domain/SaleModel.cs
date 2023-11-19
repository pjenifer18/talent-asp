using System;
using System.ComponentModel.DataAnnotations;

namespace Talent.Models.Domain
{
    public class SaleModel
    {
        [Key]
        public Guid SaleId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public Guid StoreId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateSold { get; set; }

        public CustomerModel customer { get; set; }
        public ProductModel product { get; set; }
        public StoreModel store { get; set; }

    }
}


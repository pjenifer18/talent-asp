using System;
using System.ComponentModel.DataAnnotations;

namespace Talent.Models
{
    public class AddSaleModel
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateSold { get; set; }

    }
}


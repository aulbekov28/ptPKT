using System;
using System.Collections.Generic;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public class Discount : BaseEntity
    {
        public DateTime? StartPeriod { get; set; } = DateTime.Now;
        public DateTime? EndPeriod { get; set; } = new DateTime(2999, 01, 01);
        public string Description { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal DiscountPercent { get; set; }
        public ICollection<Product> DiscountProducts { get; set; }
    }
}

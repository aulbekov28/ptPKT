using ptPKT.Core.Entities;

namespace ptPKT.Core.Helpers
{
    public class DiscountProduct
    {
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

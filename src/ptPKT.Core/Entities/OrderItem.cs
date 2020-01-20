using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public ProductGood Item { get; set; }
        public int ItemId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}

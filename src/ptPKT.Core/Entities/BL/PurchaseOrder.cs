using ptPKT.SharedKernel;
using System.Collections.Generic;

namespace ptPKT.Core.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

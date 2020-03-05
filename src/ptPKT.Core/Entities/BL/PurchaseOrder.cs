using System.Collections.Generic;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public class PurchaseOrder : BaseEntity
    {
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

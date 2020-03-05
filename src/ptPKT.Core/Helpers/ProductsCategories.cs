using ptPKT.Core.Entities;
using ptPKT.Core.Entities.BL;

namespace ptPKT.Core.Helpers
{
    public class ProductsCategories
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

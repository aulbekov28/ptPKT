using System.ComponentModel.DataAnnotations;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public abstract class Product : BaseEntity
    {
        [Required]
        [StringLength(150)]
        //[Display(Name = "ProductName", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

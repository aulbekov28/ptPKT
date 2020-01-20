using ptPKT.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace ptPKT.Core.Entities
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

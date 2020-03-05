using System.ComponentModel.DataAnnotations;

namespace ptPKT.Core.Entities.BL
{
    public class ProductGood : Product
    {
        public string Manufacturer { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorCode { get; set; }
    }
}

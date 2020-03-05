using System.ComponentModel.DataAnnotations;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public class Tag : BaseEntity
    {
        [StringLength(30)]
        public string TagName { get; set; }
    }
}

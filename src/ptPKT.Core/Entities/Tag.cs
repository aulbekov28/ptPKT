using ptPKT.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace ptPKT.Core.Entities
{
    public class Tag : BaseEntity
    {
        [StringLength(30)]
        public string TagName { get; set; }
    }
}

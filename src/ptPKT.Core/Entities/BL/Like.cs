using ptPKT.Core.Entities.Identity;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities.BL
{
    public class Like : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Entity { get; set; }
    }
}

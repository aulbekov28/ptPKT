using ptPKT.Core.Identity;
using ptPKT.SharedKernel;

namespace ptPKT.Core.Entities
{
    public class Like : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Entity { get; set; }
    }
}

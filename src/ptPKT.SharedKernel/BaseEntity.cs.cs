using System;

namespace ptPKT.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool isDeleted { get; set; }
    }
}

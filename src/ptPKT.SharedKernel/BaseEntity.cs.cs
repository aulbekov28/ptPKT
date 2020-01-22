using System;

namespace ptPKT.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; }

        protected BaseEntity(TId id)
        {
            Id = id;
        }

        protected BaseEntity()
        {
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(BaseEntity<TId> other)
        {
            return other != null && Id.Equals(other.Id);
        }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
        protected BaseEntity(int id) : base(id)
        {
        }

        protected BaseEntity() 
        {
        }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public bool isDeleted { get; set; }
    }
}

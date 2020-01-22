using ptPKT.SharedKernel;

namespace ptPKT.SharedKernel.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch(BaseDomainEvent domainEvent);
    }
}
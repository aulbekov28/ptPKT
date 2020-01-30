using System;
using System.Collections.Generic;
using System.Text;
using ptPKT.SharedKernel;
using ptPKT.SharedKernel.Interfaces;

namespace ptPKT.Tests
{
    public class NoOpDomainEventDispatcher : IDomainEventDispatcher
    {
        public void Dispatch(BaseDomainEvent domainEvent) { }
    }
}

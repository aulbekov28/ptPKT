using System;
using System.Threading.Tasks;
using ptPKT.ServiceCaller.Rerefence;

namespace ptPKT.ServiceCaller.ServiceCaller
{
    public interface IServiceCallWrapper
    {
        Task WrapTmServiceCall(string remoteAddress, Func<SomeApiClient, Task> func);
    }
}
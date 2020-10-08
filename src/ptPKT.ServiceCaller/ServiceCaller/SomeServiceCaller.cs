using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ptPKT.ServiceCaller.Rerefence;

namespace ptPKT.ServiceCaller.ServiceCaller
{
    public class SomeServiceCaller : IServiceCallWrapper
    {
        private readonly ILogger _logger;

        public SomeServiceCaller(ILogger logger)
        {
            _logger = logger;
        }
        
        public async Task WrapTmServiceCall(string remoteAddress, Func<SomeApiClient, Task> func)
        {
            SomeApiClient client = null;
            try
            {
                client = new SomeApiClient(); // some init

                await client.OpenAsync(); 

                await func(client);

                await client.CloseAsync(); 
            }
            catch (Exception ex)
            {
                client?.Abort();
                _logger.LogError(ex, "ServiceCall failed.");
                throw;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ptPKT.ServiceCaller.Rerefence;

namespace ptPKT.ServiceCaller.ServiceCaller.Decorators
{
    public class RetryWrapTmServiceCallDecorator : IServiceCallWrapper
    {
        private readonly IServiceCallWrapper _serviceCallWrapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public RetryWrapTmServiceCallDecorator(IServiceCallWrapper serviceCallWrapper, IConfiguration configuration, ILogger logger)
        {
            _serviceCallWrapper = serviceCallWrapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task WrapTmServiceCall(string remoteAddress, Func<SomeApiClient, Task> func)
        {
            var retryCount = Convert.ToInt32(_configuration["retryCount"]);
            var retryInterval = Convert.ToInt32(_configuration["retryInterval"]);
            
            for (var i = 1; i <= retryCount; i++)
            {
                try
                {
                    await _serviceCallWrapper.WrapTmServiceCall(remoteAddress, func);
                    return;
                }
                catch (Exception ex)
                {
                    if (i >= retryCount)
                    {
                        _logger.LogError(ex, $"Failed to call service on attempt {i}");
                        throw;
                    }
                }
                await Task.Delay(retryInterval);
            }
            
        }
    }
}
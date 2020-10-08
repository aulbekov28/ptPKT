using Microsoft.Extensions.Configuration;

namespace ptPKT.ServiceCaller.RemoteAddressStrategy
{
    public class DefaultAddress : IRemoteAddressPath
    {
        private readonly IConfiguration _configuration;

        public DefaultAddress(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GetServiceAddress()
        {
            return _configuration["default"];
        }
    }
}
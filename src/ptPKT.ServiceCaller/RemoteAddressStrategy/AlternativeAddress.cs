namespace ptPKT.ServiceCaller.RemoteAddressStrategy
{
    public class AlternativeAddress : IRemoteAddressPath
    {
        private readonly string _altAddress;


        public AlternativeAddress(string altAddress)
        {
            _altAddress = altAddress;
        }
        
        public string GetServiceAddress()
        {
            return _altAddress;
        }
    }
}
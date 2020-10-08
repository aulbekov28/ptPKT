using System;
using Autofac;
using ptPKT.ServiceCaller.Options;
using ptPKT.ServiceCaller.RemoteAddressStrategy;
using ptPKT.ServiceCaller.RemoteService;
using ptPKT.ServiceCaller.ServiceCaller;

namespace ptPKT.ServiceCaller
{
    public class ServiceCallerModule : Module
    {
        private readonly ServiceCallerOptions _options;

        public ServiceCallerModule(ServiceCallerOptions options)
        {
            _options = options;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SomeRemoteService>().As<IRemoteService>().InstancePerDependency();
            builder.RegisterType<SomeServiceCaller>().As<IServiceCallWrapper>();
            builder.RegisterType<DefaultAddress>().As<IRemoteAddressPath>();

            if (_options.IsRetryOn)
            {
                builder.RegisterDecorator<SomeServiceCaller, IServiceCallWrapper>();
            }
        }
    }
}
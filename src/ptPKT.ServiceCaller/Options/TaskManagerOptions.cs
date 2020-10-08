namespace ptPKT.ServiceCaller.Options
{
    public class ServiceCallerOptions
    {
        public const string ServiceCallerOptionsSettingsKey = "ServiceCallerOptions";

        /// <summary>
        /// This parameters decorates the <see cref="ITmServiceCaller"/> with <see cref="TmServiceCallerRetryDecorator"/>.
        /// Enables connectionStrings local caching.
        /// </summary>
        public bool IsRetryOn { get; set; }
    }
}
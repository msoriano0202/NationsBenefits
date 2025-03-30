using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace NationsBenefits.Application.Cache
{
    public class ConnectionHelper
    {
        private static IConfiguration _config;
        public static void AppSettingsConfigure(IConfiguration config)
        {
            _config = config;
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;
        static ConnectionHelper()
        {
            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(_config.GetSection("RedisUrl").Value);
            });
        }

        public static ConnectionMultiplexer Connection
        {
            get
            { 
                return lazyConnection.Value;
            }
        }
    }
}

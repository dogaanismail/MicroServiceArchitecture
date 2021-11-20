using StackExchange.Redis;

namespace Basket.Api.Services
{
    public class RedisService
    {
        #region Fields
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _ConnectionMultiplexer;

        #endregion

        #region Ctor

        public RedisService(string host,
            int port)
        {
            _host = host;
            _port = port;
        }

        #endregion

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");

        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db);
    }
}

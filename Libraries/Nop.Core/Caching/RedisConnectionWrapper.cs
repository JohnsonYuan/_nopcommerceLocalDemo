using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using StackExchange.Redis;
using Nop.Core.Configuration;

namespace Nop.Core.Caching
{
    /// <summary>
    /// Redis connection wrapper
    /// </summary>
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {
        private readonly NopConfig _config;
        private readonly Lazy<string> _connectionString;

        private volatile ConnectionMultiplexer _connection;
        private readonly object _locker = new object();

        public RedisConnectionWrapper(NopConfig config)
        {
            this._config = config;
            this._connectionString = new Lazy<string>(GetConnectionString);
        }

        private string GetConnectionString()
        {
            return _config.RedisCachingConnectionString;
        }

        private ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_locker)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                if (_connection != null)
                {
                    //Connection disconnected. Disposing connection...
                    _connection.Dispose();
                }

                //Creating new instance of Redis Connection
                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }

            return _connection;
        }

        public IDatabase DataBase(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1); //_settings.DefaultDb);
        }

        public IServer Server(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }
        public EndPoint[] GetEndpoints()
        {
            return GetConnection().GetEndPoints();
        }

        public void FlushDb(int? db = default(int?))
        {
            var endPoints = GetEndpoints();

            foreach (var endPoint in endPoints)
            {
                Server(endPoint).FlushDatabase(db ?? -1);   //_settings.DefaultDb);
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}

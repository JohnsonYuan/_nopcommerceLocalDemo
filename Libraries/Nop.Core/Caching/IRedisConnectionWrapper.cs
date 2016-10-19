using StackExchange.Redis;
using System;
using System.Net;

namespace Nop.Core.Caching
{
    /// <summary>
    /// Redis connection wrapper
    /// </summary>
    public interface IRedisConnectionWrapper : IDisposable
    {
        IDatabase DataBase(int? db = null);
        IServer Server(EndPoint endPoint);
        EndPoint[] GetEndpoints();
        void FlushDb(int? db = null);
    }
}

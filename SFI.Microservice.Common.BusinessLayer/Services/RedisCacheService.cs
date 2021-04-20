using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public class RedisCacheService<T> : IReadService<T>, IWriteService<T> where T : DbEntity
    {
        private readonly IReadService<T> _readService;
        private readonly IWriteService<T> _writeService;
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IReadService<T> readService, IWriteService<T> writeService, IDistributedCache distributedCache)
        {
            _readService = readService;
            _writeService = writeService;
            _distributedCache = distributedCache;
        }

        public List<T> GetAll()
        {
            return _readService.GetAll();
        }

        public List<TTarget> GetAll<TTarget>()
        {
            return _readService.GetAll<TTarget>();
        }

        public T GetById(long id)
        {
            throw new NotImplementedException();
        }

        public TTarget GetById<TTarget>(int itemId, CancellationToken ct)
        {
            var itemFromCache = _distributedCache.GetString($"{typeof(T).Name}_{itemId}");
            if (itemFromCache != null)
            {
                var resolved = JsonSerializer.Deserialize<TTarget>(itemFromCache);

                if (resolved != null)
                    return resolved;
            }
            
            var result = _readService.GetById<TTarget>(itemId, ct);
            _distributedCache.SetString($"{typeof(T).Name}_{itemId}", JsonSerializer.Serialize(result));

            return result;
        }

        /// <inheritdoc />
        public T Create(T input)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T Update(T item)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool Delete(T item)
        {
            throw new NotImplementedException();
        }
    }
}

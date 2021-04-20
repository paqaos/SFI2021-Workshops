using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public class MemoryCacheService<T> : IReadService<T>, IWriteService<T> where T : DbEntity
    {
        private IMemoryCache _memoryCache;
        private IWriteService<T> _writeService;
        private IReadService<T> _readService;

        public MemoryCacheService(IMemoryCache memoryCache, IReadService<T> readService, IWriteService<T> writeService)
        {
            _memoryCache = memoryCache;
            _readService = readService;
            _writeService = writeService;
        }

        /// <inheritdoc />
        public List<T> GetAll()
        {
            return _readService.GetAll<T>();
        }

        /// <inheritdoc />
        public List<TTarget> GetAll <TTarget>()
        {
            return _readService.GetAll<TTarget>();
        }

        /// <inheritdoc />
        public T GetById(long id)
        {
            var itemFromCache = _memoryCache.TryGetValue<T>($"{typeof(T).Name}_{id}", out T value);

            if (itemFromCache)
                return value;

            var result = _readService.GetById(id);
            _memoryCache.Set($"{typeof(T).Name}_{id}", result);

            return result;
        }

        /// <inheritdoc />
        public TTarget GetById <TTarget>(int itemId, CancellationToken ct)
        {
            var itemFromCache = _memoryCache.TryGetValue<TTarget>($"{typeof(T).Name}_{itemId}", out TTarget value);

            if (itemFromCache)
                return value;

            var result = _readService.GetById<TTarget>(itemId, ct);
            _memoryCache.Set($"{typeof(T).Name}_{itemId}", result);

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

using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public class ReadService<TData> : IReadService<TData> where TData : DbEntity
    {
        private readonly IRepository<TData> _repository;
        private readonly IMapper _mapper;

        public ReadService(IRepository<TData> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public List<TData> GetAll()
        {
            return _repository.ReadAll();
        }

        public List<TTarget> GetAll<TTarget>()
        {
            var item = _repository.ReadAll();

            return _mapper.Map<List<TTarget>>(item);
        }

        /// <inheritdoc />
        public TData GetById(long id)
        {
            return _repository.Read(id);
        }

        /// <inheritdoc />
        public TTarget GetById <TTarget>(int itemId, CancellationToken ct)
        {
            var item = _repository.Read(itemId);

            return _mapper.Map<TTarget>(item);
        }
    }
}

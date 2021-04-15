using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public class WriteService<TData> : IWriteService<TData> where TData : DbEntity
    {
        private readonly IRepository<TData> _dataRepository;

        public WriteService(IRepository<TData> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        /// <inheritdoc />
        public TData Create(TData input)
        {
            return _dataRepository.Create(input);
        }

        /// <inheritdoc />
        public TData Update(TData item)
        {
            return _dataRepository.Update(item);
        }

        /// <inheritdoc />
        public bool Delete(TData item)
        {
            if (item.IsDeleted)
            {
                return false;
            }

            item.IsDeleted = true;
            Update(item);

            return true;
        }
    }
}

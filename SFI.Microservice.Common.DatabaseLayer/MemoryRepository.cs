using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Microservice.Common.DatabaseLayer
{
    public class MemoryRepository <TData> : IRepository<TData> where TData : DbEntity
    {
        private List<TData> _storage = new List<TData>();

        /// <inheritdoc />
        public List<TData> ReadAll()
        {
            return _storage;
        }

        /// <inheritdoc />
        public TData Read(long id)
        {
            return _storage.FirstOrDefault(x => x.Id == id);
        }

        /// <inheritdoc />
        public TData Create(TData input)
        {
            long maxId = _storage.Any() ? _storage.Max(x => x.Id) : 0;
            input.Id = maxId + 1;
            _storage.Add(input);

            return input;
        }

        /// <inheritdoc />
        public TData Update(TData input)
        {
            var oldItem = _storage.FindIndex(x => x.Id == input.Id);
            _storage.RemoveAt(oldItem);

            _storage.Add(input);

            return input;
        }
    }
}

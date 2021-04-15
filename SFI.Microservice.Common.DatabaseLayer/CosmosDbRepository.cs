using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace SFI.Microservice.Common.DatabaseLayer
{
    public class CosmosDbRepository<T> : IRepository<T> where T : DocumentDbEntity<T>
    {
        private CosmosClient _cosmosClient;
        private string _container;
        private string _database;

        public CosmosDbRepository(CosmosClient cosmosClient, string database, string container)
        {
            _cosmosClient = cosmosClient;
            _database = database;
            _container = container;
        }

        public T Create(T input)
        {
            throw new NotImplementedException();
        }

        public T Read(long id)
        {
            throw new NotImplementedException();
        }

        public List<T> ReadAll()
        {
            var queryContainer = _cosmosClient.GetContainer(_database, _container);

            var query = queryContainer.GetItemQueryIterator<T>(new QueryDefinition("SELECT * FROM c"),
                requestOptions: new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(typeof(T).Name)
                });

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var item = query.ReadNextAsync();
                item.Wait();
                var response = item.Result;

                results.AddRange(response.ToList());
            }

            return results;
        }

        public T Update(T input)
        {
            throw new NotImplementedException();
        }
    }
}

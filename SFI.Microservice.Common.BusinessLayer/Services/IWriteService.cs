using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public interface IWriteService<TData> where TData : DbEntity
    {
        TData Create(TData input);

        TData Update(TData item);

        bool Delete(TData item);
    }
}
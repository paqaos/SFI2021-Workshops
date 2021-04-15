using System.Collections.Generic;
using System.Threading;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public interface IReadService<TData> where TData : DbEntity
    {
        List<TData> GetAll();

        List<TTarget> GetAll <TTarget>();

        TData GetById(long id);

        TTarget GetById<TTarget>(int itemId, CancellationToken ct);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Microservice.Common.DatabaseLayer
{
    public interface IRepository<TData> where TData : DbEntity
    {
        List<TData> ReadAll();

        TData Read(long id);
        TData Create(TData input);
        TData Update(TData input);
    }
}

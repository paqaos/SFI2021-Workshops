using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Queries;

namespace SFI.Microservice.Common.BusinessLayer.CommandStack.QueryHandlers
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> ExecuteQueryAsync(TQuery queryFilters, CancellationToken ct);
    }
}

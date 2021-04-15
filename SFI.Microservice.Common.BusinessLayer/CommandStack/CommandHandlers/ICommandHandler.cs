using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Commands;

namespace SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers
{
    public interface ICommandHandler<in TCommand, TResult> where TCommand : ICommand
    {
        Task<TResult> ExecuteAsync(TCommand command, CancellationToken ct);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Events;

namespace SFI.Microservice.Common.BusinessLayer.CommandStack.EventHandlers
{
    public interface IEventHandler<T> where T : EventBase
    {
        Task HandleAsync(T eventToHandler);
    }
}

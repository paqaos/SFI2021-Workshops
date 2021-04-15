using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.EventHandlers;
using SFI.Microservice.Events.Dto.Events;

namespace SFI.Microservice.Events.BusinessLayer.CommandStack.EventHandlers
{
    public class EventCreatedEventHandler : IEventHandler<EventBaseCreatedEventBase>
    {
        /// <inheritdoc />
        public Task HandleAsync(EventBaseCreatedEventBase eventBaseToHandler)
        {
            Console.WriteLine("Siema!");
            return Task.CompletedTask;
        }
    }
}

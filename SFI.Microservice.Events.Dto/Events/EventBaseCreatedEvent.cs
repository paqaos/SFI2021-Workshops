using SFI.Microservice.Common.BusinessLayer.CommandStack.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Microservice.Events.Dto.Events
{
    public class EventBaseCreatedEvent : EventBase
    {
        /// <inheritdoc />
        public string Name => "event-created";
    }
}

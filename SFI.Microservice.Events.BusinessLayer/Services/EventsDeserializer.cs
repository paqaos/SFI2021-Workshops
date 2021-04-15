using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Events;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Events.Dto.Events;

namespace SFI.Microservice.Events.BusinessLayer.Services
{
    public class EventsDeserializer : IEventDeserializer
    {
        /// <inheritdoc />
        public EventBase DeserializeEvent(string name, string content)
        {
            switch (name)
            {
                case "event-created":
                    return JsonSerializer.Deserialize<EventBaseCreatedEventBase>(content);
                default:
                    return null;
            }
        }
    }
}

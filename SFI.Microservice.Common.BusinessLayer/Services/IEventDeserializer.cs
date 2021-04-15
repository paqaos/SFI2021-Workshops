using SFI.Microservice.Common.BusinessLayer.CommandStack.Events;

namespace SFI.Microservice.Common.BusinessLayer.Services
{
    public interface IEventDeserializer
    {
        EventBase DeserializeEvent(string name, string content);
    }
}

using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.DatabaseModel
{
    public class EventDescription : DocumentDbEntity<EventDescription>
    {
        public int EventId { get; set; }
    }
}

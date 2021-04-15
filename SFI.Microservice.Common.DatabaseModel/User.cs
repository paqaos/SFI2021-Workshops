using System.Collections.Generic;
using System.Text.Json.Serialization;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.DatabaseModel
{
    public class User : DbEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public string Nickname { get; set; }

        public virtual ICollection<EventItem> Sessions { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public bool ConfirmedUser { get; set; }
    }
}

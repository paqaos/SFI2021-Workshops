using SFI.Microservice.Common.DatabaseLayer;
using System;
using System.Collections.Generic;

namespace SFI.Microservice.Common.DatabaseModel
{
    public class EventItem : DbEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<User> Speakers { get; set; }
        
        public virtual ICollection<Participant> Participants { get; set; }
    }
}

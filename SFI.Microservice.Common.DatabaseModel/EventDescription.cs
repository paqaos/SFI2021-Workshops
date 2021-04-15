using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.DatabaseModel
{
    public class EventDescription : DocumentDbEntity<EventDescription>
    {
        public int EventId { get; set; }
    }
}

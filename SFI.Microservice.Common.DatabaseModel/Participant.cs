using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.DatabaseLayer;

namespace SFI.Microservice.Common.DatabaseModel
{
    public class Participant : DbEntity
    {
        public virtual User User { get; set; }
        public bool Confirmed { get; set; }
        public virtual EventItem Event { get; set; }
    }
}

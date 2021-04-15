using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Commands;

namespace SFI.Microservice.Participants.BusinessLayer.CommandStack.Commands
{
    public class MarkAsParticipantCommand : ICommand
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
    }
}

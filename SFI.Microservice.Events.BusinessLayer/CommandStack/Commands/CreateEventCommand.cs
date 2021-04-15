using SFI.Microservice.Common.BusinessLayer.CommandStack.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFI.Microservice.Events.BusinessLayer.CommandStack.Commands
{
    public class CreateEventCommand : ICommand
    {
        public string Name { get; set; }
    }
}

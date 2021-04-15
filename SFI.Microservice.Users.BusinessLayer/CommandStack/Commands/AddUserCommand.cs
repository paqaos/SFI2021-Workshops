using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.CommandStack.Commands;

namespace SFI.Microservice.Users.BusinessLayer.CommandStack.Commands
{
    public class AddUserCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
    }
}

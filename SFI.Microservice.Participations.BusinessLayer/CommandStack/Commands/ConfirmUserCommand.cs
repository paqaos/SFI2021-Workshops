using SFI.Microservice.Common.BusinessLayer.CommandStack.Commands;

namespace SFI.Microservice.Participants.BusinessLayer.CommandStack.Commands
{
    public class ConfirmUserCommand : ICommand
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}

using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Participants.BusinessLayer.CommandStack.Commands;
using SFI.Microservice.Common.DatabaseModel;

namespace SFI.Microservice.Participants.BusinessLayer.CommandStack.CommandHandlers
{
    public class MarkAsParticipantCommandHandler : ICommandHandler<MarkAsParticipantCommand, bool>
    {
        private readonly IReadService<Participant> _participantReadService;
        private readonly IWriteService<Participant> _participantWriteService;

        public MarkAsParticipantCommandHandler(IReadService<Participant> participantReadService, IWriteService<Participant> participantWriteService)
        {
            _participantReadService = participantReadService;
            _participantWriteService = participantWriteService;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(MarkAsParticipantCommand command, CancellationToken ct)
        {
            var existing = _participantReadService.GetAll();

            var existingConnection =
                existing.FirstOrDefault(x => x.Event.Id == command.EventId && x.User.Id == command.UserId);

            if (existingConnection != null)
            {
                return false;
            }

            _participantWriteService.Create(new Participant
            {
                Event = new EventItem
                {
                    Id = command.EventId
                },
                User = new User
                {
                    Id = command.UserId
                }
            });

            return true;
        }
    }
}

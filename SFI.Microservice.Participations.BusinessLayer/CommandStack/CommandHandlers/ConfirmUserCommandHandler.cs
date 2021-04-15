using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Participants.BusinessLayer.CommandStack.Commands;

namespace SFI.Microservice.Events.BusinessLayer.CommandStack.CommandHandlers
{
    public class ConfirmUserCommandHandler : ICommandHandler<ConfirmUserCommand, bool>
    {
        private IReadService<Participant>  _participantReadService;
        private IWriteService<Participant> _participantWriteService;

        public ConfirmUserCommandHandler(IReadService<Participant> participantReadService, IWriteService<Participant> participantWriteService)
        {
            _participantReadService = participantReadService;
            _participantWriteService = participantWriteService;
        }

        /// <inheritdoc />
        public async Task<bool> ExecuteAsync(ConfirmUserCommand command, CancellationToken ct)
        {
            var existing = _participantReadService.GetAll();

            var existingConnection =
                existing.FirstOrDefault(x => x.Event.Id == command.EventId && x.User.Id == command.UserId);

            if (existingConnection == null || existingConnection.Confirmed)
            {
                return false;
            }

            _participantWriteService.Update(new Participant
            {
                Id = existingConnection.Id,
                Confirmed = true,
                Event = new EventItem
                {
                    Id = existingConnection.Event.Id
                },
                User= new User
                {
                    Id = existingConnection.User.Id
                }
            });

            return true;
        }
    }
}

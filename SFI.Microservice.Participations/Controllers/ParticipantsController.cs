using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFI.Microservice.Common.BusinessLayer.CommandStack.CommandHandlers;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Common.DatabaseModel;
using SFI.Microservice.Participants.BusinessLayer.CommandStack.Commands;

namespace SFI.Microservice.Participants.Controllers
{
    [Route("api/participants")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IReadService<Participant>                       _participantReadService;
        private readonly ICommandHandler<MarkAsParticipantCommand, bool> _addParticipatorCommandHandler;
        private readonly ICommandHandler<ConfirmUserCommand, bool>       _confirmParticipatorCommandHandler;

        public ParticipantsController(IReadService<Participant> participantReadService, ICommandHandler<MarkAsParticipantCommand, bool> addParticipatorCommandHandler, ICommandHandler<ConfirmUserCommand, bool> confirmParticipatorCommandHandler)
        {
            _participantReadService = participantReadService;
            _addParticipatorCommandHandler = addParticipatorCommandHandler;
            _confirmParticipatorCommandHandler = confirmParticipatorCommandHandler;
        }

        [HttpGet]
        public List<Participant> GetAllParticipants()
        {
            return _participantReadService.GetAll();
        }

        [HttpPost("~/api/events/{eventId:int}/users/{userId:int}")]
        public async Task<IActionResult> MarkAsParticipatorAsync(int eventId, int userId)
        {
            var result = await _addParticipatorCommandHandler.ExecuteAsync(new MarkAsParticipantCommand
            {
                UserId = userId,
                EventId = eventId
            }, CancellationToken.None);

            if (result)
                return Accepted();

            return BadRequest();
        }

        [HttpPost("~/api/events/{eventId:int}/users/{userId:int}/confirm")]
        public async Task<IActionResult> ConfirmParticipatorAsync(int eventId, int userId)
        {
            var result = await _confirmParticipatorCommandHandler.ExecuteAsync(new ConfirmUserCommand
            {
                UserId = userId,
                EventId = eventId
            }, CancellationToken.None);

            if (result)
                return Accepted();

            return BadRequest();
        }
    }
}

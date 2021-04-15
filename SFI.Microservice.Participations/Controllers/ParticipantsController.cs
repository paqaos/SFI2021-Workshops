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
        [HttpGet]
        public List<Participant> GetAllParticipants()
        {
            return new List<Participant>();
        }
    }
}
